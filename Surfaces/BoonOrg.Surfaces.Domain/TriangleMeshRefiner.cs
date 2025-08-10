// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Logic;

namespace BoonOrg.Surfaces.Domain
{
    public class TriangleMeshRefiner
    {
        private readonly IVectorLogic m_vectorLogic;
        private readonly IVectorCreator m_vectorCreator;

        public TriangleMeshRefiner(IVectorLogic vectorLogic, IVectorCreator vectorCreator)
        {
            m_vectorLogic = vectorLogic;
            m_vectorCreator = vectorCreator;
        }

        public IndexedTriangleMesh Refine(IndexedTriangleMesh triangleMesh)
        {
            var map = new Dictionary<IVector, int>();
            var vertices = new List<IVector>();
            var triangles = new List<IndexedTriangle>();

            vertices.AddRange(triangleMesh.Vertices);
            int i = 0;
            vertices.ForEach(x => map.Add(x, i++));

            foreach (var triangle in triangleMesh.Elements)
            {
                var vertex1 = triangleMesh.Vertices[triangle.Index1];
                var vertex2 = triangleMesh.Vertices[triangle.Index2];
                var vertex3 = triangleMesh.Vertices[triangle.Index3];

                var vertexA = m_vectorLogic.Span(0.5, 0.5, vertex1, vertex2);
                var vertexB = m_vectorLogic.Span(0.5, 0.5, vertex2, vertex3);
                var vertexC = m_vectorLogic.Span(0.5, 0.5, vertex3, vertex1);

                int triangleIndexA = -1;
                int triangleIndexB = -1;
                int triangleIndexC = -1;
                var vertex = map.Keys.SingleOrDefault(x => m_vectorLogic.AlmostEquals(x, vertexA));
                if (vertex == null)
                {
                    triangleIndexA = vertices.Count;
                    vertices.Add(m_vectorCreator.Create(vertexA));
                }
                else
                {
                    triangleIndexA = map[vertex];
                }
                vertex = map.Keys.SingleOrDefault(x => m_vectorLogic.AlmostEquals(x, vertexB));
                if (vertex == null)
                {
                    triangleIndexB = vertices.Count;
                    vertices.Add(m_vectorCreator.Create(vertexB));
                }
                else
                {
                    triangleIndexB = map[vertex];
                }
                vertex = map.Keys.SingleOrDefault(x => m_vectorLogic.AlmostEquals(x, vertexC));
                if (vertex == null)
                {
                    triangleIndexC = vertices.Count;
                    vertices.Add(m_vectorCreator.Create(vertexC));
                }
                else
                {
                    triangleIndexC = map[vertex];
                }

                triangles.Add(new IndexedTriangle(triangle.Index1, triangleIndexA, triangleIndexC));
                triangles.Add(new IndexedTriangle(triangleIndexA, triangle.Index2, triangleIndexB));
                triangles.Add(new IndexedTriangle(triangleIndexC, triangleIndexB, triangle.Index3));
                triangles.Add(new IndexedTriangle(triangleIndexC, triangleIndexA, triangleIndexB));
            }

            return new IndexedTriangleMesh(vertices.ToArray(), triangles.ToArray());
        }
    }
}
