// (c) 2017,2022 Roland Boon

using System;
using System.Collections.Generic;
using BoonOrg.Geometry.Generators;

namespace BoonOrg.Geometry.Domain.Generators
{
    internal sealed class LayerGridToMeshGeometryGenerator : IMeshGeometryGenerator<ILayerGrid>
    {
        private readonly IBasicMeshGeometryGenerator m_meshGeometryGenerator;

        public LayerGridToMeshGeometryGenerator(IBasicMeshGeometryGenerator meshGeometryGenerator)
        {
            m_meshGeometryGenerator = meshGeometryGenerator;
        }

        public IEnumerable<IMeshGeometry> Generate(IGeometry geometry)
        {
            return Generate((ILayerGrid)geometry);
        }

        public IEnumerable<IMeshGeometry> Generate(ILayerGrid grid)
        {
            m_meshGeometryGenerator.Reset();

            // Make the surface's points and triangles.
            foreach (IPoint[] cell in grid.Cells)
            {
                var node0 = cell[0];
                var node1 = cell[1];
                var node2 = cell[2];
                var node3 = cell[3];

                var index0 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(node0);
                var index1 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(node1);
                var index2 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(node2);
                var index3 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(node3);

                m_meshGeometryGenerator.AddTriangle(index0, index2, index1);
                m_meshGeometryGenerator.AddTriangle(index0, index3, index2);
            }

            m_meshGeometryGenerator.AddTextureCoordinates(grid.Area);

            return new List<IMeshGeometry> { m_meshGeometryGenerator.Mesh };
        }

        public bool HandlesType(Type type)
        {
            return typeof(ILayerGrid).IsAssignableFrom(type);
        }
    }
}
