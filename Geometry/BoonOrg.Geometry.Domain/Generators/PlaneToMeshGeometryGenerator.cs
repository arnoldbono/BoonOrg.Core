// (c) 2017,2022 Roland Boon

using System;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Geometry.Generators;

namespace BoonOrg.Geometry.Domain.Generators
{
    internal sealed class PlaneToMeshGeometryGenerator : IMeshGeometryGenerator<IPlane>
    {
        private readonly IBasicMeshGeometryGenerator m_meshGeometryGenerator;

        public PlaneToMeshGeometryGenerator(IBasicMeshGeometryGenerator meshGeometryGenerator)
        {
            m_meshGeometryGenerator = meshGeometryGenerator;
        }

        public IEnumerable<IMeshGeometry> Generate(IGeometry geometry)
        {
            return Generate((IPlane)geometry);
        }

        public IEnumerable<IMeshGeometry> Generate(IPlane plane)
        {
            m_meshGeometryGenerator.Reset();

            IPoint[] vertices = plane.Vertices.ToArray();

            // Make the surface's points and triangles.
            var node0 = vertices[0];
            var node1 = vertices[1];
            var node2 = vertices[2];
            var node3 = vertices[3];

            var index0 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(node0);
            var index1 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(node1);
            var index2 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(node2);
            var index3 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(node3);

            m_meshGeometryGenerator.AddTriangle(index0, index2, index1);
            m_meshGeometryGenerator.AddTriangle(index0, index3, index2);
        
            return new List<IMeshGeometry> { m_meshGeometryGenerator.Mesh };
        }

        public bool HandlesType(Type type)
        {
            return typeof(IPlane).IsAssignableFrom(type);
        }
    }
}
