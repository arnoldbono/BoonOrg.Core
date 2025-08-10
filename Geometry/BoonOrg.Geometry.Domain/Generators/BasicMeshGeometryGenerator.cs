// (c) 2017, 2022, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Generators;

namespace BoonOrg.Geometry.Domain.Generators
{
    public class BasicMeshGeometryGenerator : IBasicMeshGeometryGenerator
    {
        // A dictionary to hold points for fast lookup.
        private Dictionary<IPoint, int> m_nodeIndexMap;
        private MeshGeometry m_mesh;

        public IMeshGeometry Mesh => m_mesh;

        public BasicMeshGeometryGenerator()
        {
            Reset();
        }

        public void Reset()
        {
            // Make a mesh to hold the surface.
            m_nodeIndexMap = new Dictionary<IPoint, int>();
            m_mesh = new MeshGeometry();
        }

        public int AddOrReuseNodeGetIndex(IPoint node)
        {
            if (m_nodeIndexMap.ContainsKey(node))
            {
                return m_nodeIndexMap[node];
            }

            int index = AddNodeGetIndex(node);
            m_nodeIndexMap.Add(node, index);

            return index;
        }

        public int AddNodeGetIndex(IPoint node)
        {
            int index = m_mesh.PositionCount;
            m_mesh.AddPosition(node);
            return index;
        }

        public void AddTextureCoordinates(IArea area)
        {
            foreach (var position in m_mesh.Positions)
            {
                var pt = new Point(area.GetNormalizedX(position.X), area.GetNormalizedY(position.Y), 0.0);
                m_mesh.AddTextureCoordinates(pt);
            }
        }

        public void AddTriangles(ITriangleContainer container, bool facetedTriangles)
        {
            if (facetedTriangles)
            {
                foreach (ITriangle triangle in container.Triangles)
                {
                    int index1 = AddNodeGetIndex(triangle.Vertex1);
                    int index2 = AddNodeGetIndex(triangle.Vertex2);
                    int index3 = AddNodeGetIndex(triangle.Vertex3);

                    AddTriangle(index1, index2, index3);
                }
            }
            else
            {
                AddTriangles(container.TriangleVertexIndices, container.Vertices);
            }
        }

        private void AddTriangles(IEnumerable<int> triangleVertexIndices, IEnumerable<IPoint> triangleVertices)
        {
            var offset = m_nodeIndexMap.Count;
            var count = offset;
            foreach (var point in triangleVertices)
            {
                m_mesh.AddPosition(point);
                m_nodeIndexMap.Add(point, count++);
            }
            foreach (var index in triangleVertexIndices)
            {
                m_mesh.AddTriangleIndex(offset + index);
            }
        }

        public void AddTriangle(int index1, int index2, int index3)
        {
            m_mesh.AddTriangleIndex(index1);
            m_mesh.AddTriangleIndex(index2);
            m_mesh.AddTriangleIndex(index3);
        }

        public void AddTriangle(ITriangle triangle)
        {
            int index1 = AddOrReuseNodeGetIndex(triangle.Vertex1);
            int index2 = AddOrReuseNodeGetIndex(triangle.Vertex2);
            int index3 = AddOrReuseNodeGetIndex(triangle.Vertex3);

            AddTriangle(index1, index2, index3);
        }

        public void ClearTriangleIndices() => m_mesh.ClearTriangleIndices();

        public IEnumerable<IMeshGeometry> Generate(IGeometry _)
        {
            return new List<IMeshGeometry> { Mesh };
        }

        public bool HandlesType(Type type)
        {
            return false;
        }
    }
}
