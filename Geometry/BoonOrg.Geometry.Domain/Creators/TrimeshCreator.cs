// (c) 2017, 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class TrimeshCreator : ITrimeshCreator
    {
        private readonly Func<ITrimesh> m_trimeshFunc;
        private readonly ITrimeshPropertyNormalsService m_normalsService;

        private readonly List<IPoint> m_vertices = new();
        private readonly List<IVector> m_normals = new();
        private readonly List<(int index1, int index2, int index3)> m_triangles = new();

        private int m_checkForDuplicateCount = 0;
        private int m_checkForDuplicateIndex = 0;
        private List<IPoint> m_checkForDuplicates = new();

        public TrimeshCreator(Func<ITrimesh> trimeshFunc, ITrimeshPropertyNormalsService normalsService)
        {
            m_trimeshFunc = trimeshFunc;
            m_normalsService = normalsService;
        }

        public ITrimesh Create()
        {
            return m_trimeshFunc();
        }

        public ITrimesh Create(string name)
        {
            var indices = new List<int>(m_triangles.Count * 3);

            foreach (var (index1, index2, index3) in m_triangles)
            {
                indices.Add(index1);
                indices.Add(index2);
                indices.Add(index3);
            }

            return Create(indices, m_vertices, m_normals, name);
        }

        public ITrimesh Create(IEnumerable<ITriangle> triangles, string name)
        {
            var trimesh = Create();
            trimesh.Add(triangles);
            trimesh.Identification.Rename(name);
            return trimesh;
        }

        public ITrimesh Create(IEnumerable<int> triangleVertexIndices, IEnumerable<IPoint> vertices, string name)
        {
            var trimesh = Create();
            trimesh.Add(triangleVertexIndices, vertices);
            trimesh.Identification.Rename(name);
            return trimesh;
        }

        public ITrimesh Create(IEnumerable<int> triangleVertexIndices, IEnumerable<IPoint> vertices, IEnumerable<IVector> normals, string name)
        {
            var trimesh = Create();
            trimesh.Add(triangleVertexIndices, vertices);
            m_normalsService.AddNormals(trimesh, normals);
            trimesh.Identification.Rename(name);
            return trimesh;
        }

        public int AddVertex(IPoint vertex)
        {
            var count = m_vertices.Count;
            var indexDuplicate = 0;
            var accuracy = 0.00000001;

            if (m_checkForDuplicateCount != 0)
            {
                indexDuplicate = m_checkForDuplicateIndex;
                foreach (var point in m_checkForDuplicates)
                {
                    if (point.IsSame(vertex, accuracy))
                    {
                        return indexDuplicate;
                    }

                    ++indexDuplicate;
                }
            }

            for (; indexDuplicate < count; ++indexDuplicate)
            {
                var point = m_vertices[indexDuplicate];
                if (point.IsSame(vertex, accuracy))
                {
                    return indexDuplicate;
                }
            }

            m_vertices.Add(vertex);
            return count;
        }

        public int AddVertex(IPoint vertex, IVector normal)
        {
            var count = m_vertices.Count;
            int index = AddVertex(vertex);

            // Was vertex added to the list?
            if (index == count)
            {
                m_normals.Add(normal);
            }

            return index;
        }

        public void AddTriangle(int index1, int index2, int index3, bool reverseOrientation)
        {
            // var p1 = m_vertices[index1];
            // var p2 = m_vertices[index2];
            // var p3 = m_vertices[index3];
            // var n2 = m_normals[index2];
            // reverseOrientation = m_vectorLogic.GetInnerProduct(m_vectorLogic.GetCrossProduct(m_vectorLogic.Subtract(p2, p3), m_vectorLogic.Subtract(p2, p1)), n2) < 0.0
            if (reverseOrientation)
            {
                m_triangles.Add(new (index3, index2, index1));
            }
            else
            {
                m_triangles.Add(new (index1, index2, index3));
            }
        }

        public void AddTriangle(int index1, int index2, int index3)
        {
            m_triangles.Add(new(index1, index2, index3));
        }

        public void AddTrimesh(ITrimesh trimesh)
        {
            var offset = m_vertices.Count;

            var normals = m_normalsService.GetNormals(trimesh)?.ToArray() ?? null;

            if (normals != null)
            {
                var i = 0;
                foreach (var vertex in trimesh.Vertices)
                {
                    m_vertices.Add(vertex);
                    m_normals.Add(normals[i++]);
                }
            }
            else
            {
                foreach (var vertex in trimesh.Vertices)
                {
                    m_vertices.Add(vertex);
                }
            }

            var indices = trimesh.TriangleVertexIndices.ToArray();
            var triangleCount = indices.Length / 3;
            var index = 0;
            for (var triangle = 0; triangle < triangleCount; ++triangle)
            {
                m_triangles.Add((indices[index++] + offset, indices[index++] + offset, indices[index++] + offset));
            }
        }

        public void UpdateIndexCheckForDuplicates()
        {
            var count = m_vertices.Count;
            if (m_checkForDuplicateCount == 0)
            {
                m_checkForDuplicateCount = count;
            }

            m_checkForDuplicateIndex = count - m_checkForDuplicateCount;
            m_checkForDuplicates = m_vertices.GetRange(m_checkForDuplicateIndex, m_checkForDuplicateCount);
        }

        public void Clear()
        {
            m_vertices.Clear();
            m_normals.Clear();
            m_triangles.Clear();
            m_checkForDuplicates.Clear();
        }
    }
}
