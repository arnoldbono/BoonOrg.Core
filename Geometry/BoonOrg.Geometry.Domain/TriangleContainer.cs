// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    public class TriangleContainer : Surface, ITriangleContainer
    {
        // Vertex. Each angular point of a polygon, polyhedron, or other figure is called a vertex.

        protected readonly List<int> m_triangleVertexIndices = new();
        protected readonly List<IPoint> m_vertices = new();
        protected readonly List<IVector> m_normals = new();
        private readonly Func<IBoundingBox> m_boundingBoxFunc;

        virtual public IEnumerable<ITriangle> Triangles
        {
            get
            {
                int count = TriangleCount;
                for (int index = 0; index < count; ++index)
                {
                    yield return GetTriangle(index);
                }
            }
        }

        virtual public IEnumerable<int> TriangleVertexIndices => m_triangleVertexIndices;

        virtual public IEnumerable<IPoint> Vertices => m_vertices;

        virtual public IEnumerable<IVector> Normals => m_normals;

        virtual public int TriangleCount => m_triangleVertexIndices.Count / 3;

        public TriangleContainer(IIdentity identity, Func<IBoundingBox> boundingBoxFunc) : base(identity)
        {
            m_boundingBoxFunc = boundingBoxFunc;
        }

        public void Add(IEnumerable<ITriangle> triangles)
        {
            AddTriangles(triangles);
        }

        public void Add(IEnumerable<int> triangleVertexIndices, IEnumerable<IPoint> vertices)
        {
            m_triangleVertexIndices.AddRange(triangleVertexIndices);
            m_vertices.AddRange(vertices);
        }

        public void Add(IEnumerable<IVector> normals)
        {
            m_normals.AddRange(normals);
        }

        public ITriangle GetTriangle(int index)
        {
            index *= 3;
            int count = m_triangleVertexIndices.Count;
            if (index >= count || index + 2 >= count)
            {
                return null;
            }

            var vertex1 = m_vertices[m_triangleVertexIndices[index]];
            var vertex2 = m_vertices[m_triangleVertexIndices[index + 1]];
            var vertex3 = m_vertices[m_triangleVertexIndices[index + 2]];

            var triangle = new Triangle(m_boundingBoxFunc);
            triangle.Assign(vertex1, vertex2, vertex3);
            return triangle;
        }

        public IPoint GetTriangleVertex(int index)
        {
            int count = m_triangleVertexIndices.Count;
            return (index < count) ? m_vertices[index] : null;
        }

        public void AddTriangle(IPoint vertex1, IPoint vertex2, IPoint vertex3)
        {
            AddTriangleVertex(vertex1);
            AddTriangleVertex(vertex2);
            AddTriangleVertex(vertex3);
        }

        public void AddTriangles(IEnumerable<ITriangle> triangles)
        {
            foreach (var t in triangles)
            {
                AddTriangle(t);
            }
        }

        public void AddTriangle(ITriangle triangle)
        {
            AddTriangle(triangle.Vertex1, triangle.Vertex2, triangle.Vertex3);
        }

        private void AddTriangleVertex(IPoint vertex)
        {
            int index = m_vertices.Count;
            m_vertices.Add(vertex);
            m_triangleVertexIndices.Add(index);
        }

        //public double ComputeArea()
        //{
        //    return Triangles.Sum(t => t.ComputeArea());
        //}

        protected void SetCapacity(int vertexCount, int triangleCount)
        {
            m_vertices.Capacity = vertexCount;
            m_normals.Capacity = vertexCount;
            for (int i = 0; i < vertexCount; ++i)
            {
                m_vertices.Add(new Point());
                m_normals.Add(new Vector());
            }

            int count = triangleCount * 3;
            m_triangleVertexIndices.Capacity = count;
            for (int i = 0; i < count; ++i)
            {
                m_triangleVertexIndices.Add(0);
            }
        }

        protected void SetTriangle(int index, int vertextIndex1, int vertextIndex2, int vertextIndex3)
        {
            index *= 3;
            m_triangleVertexIndices[index] = vertextIndex1;
            m_triangleVertexIndices[index + 1] = vertextIndex2;
            m_triangleVertexIndices[index + 2] = vertextIndex3;
        }

        protected void SetVertex(int index, double x, double y, double z)
        {
            var vertex = m_vertices[index];
            vertex.X = x;
            vertex.Y = y;
            vertex.Z = z;
        }

        protected void SetNormal(int index, double x, double y, double z)
        {
            var vertex = m_normals[index];
            vertex.X = x;
            vertex.Y = y;
            vertex.Z = z;
        }

        public override IBoundingBox GetBoundingBox()
        {
            var boudingBox = m_boundingBoxFunc();
            boudingBox.Expand(m_vertices);
            return boudingBox;
        }

        public override void ExpandBoundingBox(IBoundingBox box)
        {
            box.Expand(m_vertices);
        }

        public virtual void Clear()
        {
            m_triangleVertexIndices.Clear();
            m_vertices.Clear();
        }
    }
}
