// (c) 2017, 2022, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Generators;
using BoonOrg.Geometry;
using BoonOrg.Geometry.Creators;

namespace BoonOrg.Surfaces.Domain
{
    public sealed class Grid2DToMeshGeometryGenerator : IMeshGeometryGenerator<IGrid2D>
    {
        private readonly IBasicMeshGeometryGenerator m_meshGeometryGenerator;
        private readonly IVectorCreator m_vectorCreator;

        public Grid2DToMeshGeometryGenerator(IBasicMeshGeometryGenerator meshGeometryGenerator,
            IVectorCreator vectorCreator)
        {
            m_meshGeometryGenerator = meshGeometryGenerator;
            m_vectorCreator = vectorCreator;
        }

        public IEnumerable<IMeshGeometry> Generate(IGeometry geometry)
        {
            return Generate((IGrid2D)geometry);
        }

        public IEnumerable<IMeshGeometry> Generate(IGrid2D grid)
        {
            m_meshGeometryGenerator.Reset();

            var center = grid.Center;
            var lengthSide1 = grid.LengthSide1;
            var lengthSide2 = grid.LengthSide2;
            var vertexCountSide1 = grid.VertexCountSide1 - 1;
            var vertexCountSide2 = grid.VertexCountSide2 - 1;

            var vertices = new List<IVector>();

            for (var i = 0; i <= vertexCountSide1; ++i)
            {
                var fraction1 = i / (double)vertexCountSide1;

                for (var j = 0; j <= vertexCountSide2; ++j)
                {
                    var fraction2 = j / (double)vertexCountSide2;

                    var x = center.X - lengthSide1 / 2.0 + fraction1 * lengthSide1;
                    var y = center.Y - lengthSide2 / 2.0 + fraction2 * lengthSide2;
                    var z = center.Z + Math.Sin(x * Math.PI) * Math.Cos(y * Math.PI) / 4.0;
                    vertices.Add(m_vectorCreator.Create(x, y, z));
                }
            }

            var triangles = new List<IndexedTriangle>();
            for (var i = 0; i < vertexCountSide1; ++i)
            {
                var indexI0J0 = i * (vertexCountSide2 + 1);
                var indexI1J0 = (i + 1) * (vertexCountSide2 + 1);

                for (var j = 0; j < vertexCountSide2; ++j)
                {
                    var indexI0J1 = indexI0J0 + 1;
                    var indexI1J1 = indexI1J0 + 1;

                    triangles.Add(new IndexedTriangle(indexI0J0, indexI1J0, indexI1J1));
                    triangles.Add(new IndexedTriangle(indexI1J1, indexI0J1, indexI0J0));

                    ++indexI0J0;
                    ++indexI1J0;
                }
            }

            foreach (var vertex in vertices)
            {
                m_meshGeometryGenerator.AddNodeGetIndex(vertex);
            }

            foreach (var triangle in triangles)
            {
                m_meshGeometryGenerator.AddTriangle(triangle.Index1, triangle.Index2, triangle.Index3);
            }

            return new List<IMeshGeometry> { m_meshGeometryGenerator.Mesh };
        }

        public bool HandlesType(Type type)
        {
            return typeof(IGrid2D).IsAssignableFrom(type);
        }
    }
}
