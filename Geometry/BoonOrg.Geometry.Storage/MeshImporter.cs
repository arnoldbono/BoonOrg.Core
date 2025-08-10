// (c) 2015, 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Storage
{
    internal sealed class MeshImporter : IMeshImporter
    {
        // The text file contains a mesh, a set of points and triangles.
        // We start reading after line "point=dddd" to read all the vertices.
        // Next, we read all the triangles after the line "tria3=ddddd"
        // Each line with a point ends with ;0.
        // The values are separated by a semicolon.

        private readonly IPointCreator m_pointCreator;
        private readonly ITrimeshCreator m_trimeshCreator;

        public MeshImporter(IPointCreator pointCreator, ITrimeshCreator trimeshCreator)
        {
            m_pointCreator = pointCreator;
            m_trimeshCreator = trimeshCreator;
        }

        /// <inheritdoc/>
        public ITrimesh Import(string path, string name)
        {
            // Assume path exists. Exception thrown to caller.
            using (TextReader reader = File.OpenText(path))
            {
                return Import(reader, name);
            }
        }

        public ITrimesh Import(TextReader reader, string name)
        {
            var triangleVertices = new List<IPoint>();
            var triangleVertexIndices = new List<int>();

            int pointCount = 0;
            int pointIndex = 0;
            int triangleCount = 0;
            int triangleIndex = 0;

            while (true)
            {
                string line = reader.ReadLine();

                // Did we read all the lines from the file?
                if (null == line || line.Length == 0)
                {
                    break;
                }

                if (pointCount == 0)
                {
                    string prefix = @"point=";
                    if (line.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        line = line.Substring(prefix.Length);
                        pointCount = int.Parse(line);
                    }
                    continue;
                }

                if (pointIndex < pointCount)
                {
                    ++pointIndex;

                    string[] numbers = line.Split(';');
                    double x = double.Parse(numbers[0]);
                    double y = double.Parse(numbers[1]);
                    double z = double.Parse(numbers[2]);
                    if (Math.Abs(x) > 5 || Math.Abs(y) > 5 || Math.Abs(z) > 5)
                    {
                        continue;
                    }
                    triangleVertices.Add(m_pointCreator.Create(x, y, z));
                    continue;
                }

                if (triangleCount == 0)
                {
                    string prefix = @"tria3=";
                    if (line.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        line = line.Substring(prefix.Length);
                        triangleCount = int.Parse(line);
                    }
                    continue;
                }

                if (triangleIndex < triangleCount)
                {
                    ++triangleIndex;

                    string[] numbers = line.Split(';');
                    triangleVertexIndices.Add(int.Parse(numbers[0]));
                    triangleVertexIndices.Add(int.Parse(numbers[1]));
                    triangleVertexIndices.Add(int.Parse(numbers[2]));
                    continue;
                }

                break;
            }

            return m_trimeshCreator.Create(triangleVertexIndices, triangleVertices, Array.Empty<IVector>(), name);
        }

    }
}
