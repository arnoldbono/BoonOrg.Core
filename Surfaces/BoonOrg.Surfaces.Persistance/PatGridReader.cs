// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BoonOrg.Geometry.Creators;

using BoonOrg.Surfaces.Domain;

namespace BoonOrg.Surfaces.Persistence
{
    // ROBOCOP Unused
    public class PatGridReader
    {
        private readonly IConvertToIndexedTriangleMesh m_convertToIndexedTriangleMesh;
        private readonly Func<ISurfaceRepresentation> m_geometryRepresentationFunc;
        private readonly IVectorCreator m_vectorCreator;

        public PatGridReader(IConvertToIndexedTriangleMesh convertToIndexedTriangleMesh,
            Func<ISurfaceRepresentation> geometryRepresentationFunc,
            IVectorCreator vectorCreator)
        {
            m_convertToIndexedTriangleMesh = convertToIndexedTriangleMesh;
            m_geometryRepresentationFunc = geometryRepresentationFunc;
            m_vectorCreator = vectorCreator;
        }

        public ISurfaceRepresentation Read(string path)
        {
            var lines = File.ReadLines(path);
            var rows = new List<float[]>();
            foreach (var line in lines)
            {
                var cleanedLine = line.Trim();
                var parseLine = cleanedLine;
                while (true)
                {
                    cleanedLine = parseLine.Replace("  ", " ");
                    if (cleanedLine != parseLine)
                    {
                        parseLine = cleanedLine;
                        continue;
                    }

                    break;
                }
                var columns = parseLine.Split(' ');
                rows.Add(columns.Select(x => float.Parse(x)).ToArray());
            }

            float minimum = float.MaxValue;
            float maximum = float.MinValue;
            var columnCount = 0;
            var rowCount = rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                var columns = rows[i];

                if (columns.Length != columnCount)
                {
                    if (columnCount == 0)
                    {
                        columnCount = columns.Length;
                    }
                    else
                    {
                        throw new InvalidDataException($@"Row {i + 1} has incorrect number of columns");
                    }
                }

                for (int j = 0; j < columnCount; j++)
                {
                    var col = columns[j];
                    if (col < minimum)
                    {
                        minimum = col;
                    }
                    if (col > maximum)
                    {
                        maximum = col;
                    }
                }
            }

            var ratio = (rowCount - 1) / (columnCount - 1);
            var grid = new Grid2D(m_vectorCreator.Create(), m_vectorCreator.Create(0.0, 0.0, 1.0), ratio * 2.0, 2.0,
                rowCount, columnCount);
            var mesh = m_convertToIndexedTriangleMesh.Convert(grid, false);
            var vertices = mesh.Vertices;
            for (int i = 0; i < rowCount; i++)
            {
                var columns = rows[i];
                for (int j = 0; j < columnCount; j++)
                {
                    var vertex =
                    vertices[i * columnCount + j].Z = (columns[j] - minimum) / (maximum - minimum) - 0.5;
                }
            }

            var geometryRepresentation = m_geometryRepresentationFunc();
            geometryRepresentation.Mesh = mesh;
            return geometryRepresentation;
        }
    }
}
