// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Generators;
using BoonOrg.Geometry.Logic;

namespace BoonOrg.Surfaces.Domain
{
    public class ConvertToIndexedTriangleMesh : IConvertToIndexedTriangleMesh
    {
        private readonly IMeshGeometryService m_meshGeometryService;
        private readonly IBasicMeshGeometryGenerator m_meshGeometryGenerator;
        private readonly IVectorCreator m_vectorCreator;
        private readonly IVectorLogic m_vectorLogic;

        public ConvertToIndexedTriangleMesh(IMeshGeometryService meshGeometryService,
            IBasicMeshGeometryGenerator meshGeometryGenerator,
            IVectorCreator vectorCreator,
            IVectorLogic vectorLogic)
        {
            m_meshGeometryService = meshGeometryService;
            m_meshGeometryGenerator = meshGeometryGenerator;
            m_vectorCreator = vectorCreator;
            m_vectorLogic = vectorLogic;
        }

        public IIndexedMesh<IndexedTriangle> Convert(IGeometry geometry, bool facetedTriangles)
        {
            var meshGeometries = m_meshGeometryService.Generate(geometry);
            if (meshGeometries == null)
            {
                return null;
            }

            if (facetedTriangles)
            {
                m_meshGeometryGenerator.Reset();

                foreach (var meshGeometry in meshGeometries)
                {
                    var positions = meshGeometry.Positions.ToArray();
                    var triangleIndices = meshGeometry.TriangleIndices.ToArray();
                    var index = 0;
                    var indices = new int[] { 0, 0, 0 };
                    for (var i = 0; i < meshGeometry.TriangleCount; ++i)
                    {
                        for (var j = 0; j < 3; ++j)
                        {
                            indices[j] = m_meshGeometryGenerator.AddNodeGetIndex(positions[triangleIndices[index + j]]);
                        }

                        index += 3;

                        m_meshGeometryGenerator.AddTriangle(indices[0], indices[1], indices[2]);
                    }
                }

                meshGeometries = m_meshGeometryGenerator.Generate(null);
            }

            var vertices = new List<IVector>();
            var normals = new List<IVector>();
            var triangles = new List<IndexedTriangle>();

            // ROBOCOP This is a lengthy computation. We need to be able to stop it in case the visibility is revoked.
            // ROBOCOP If we want the same normal for the whole triangle, then we need to duplicate the vertices.
            foreach (var meshGeometry in meshGeometries)
            {
                foreach (var point in meshGeometry.Positions)
                {
                    vertices.Add(m_vectorCreator.Create(point));
                }

                foreach (var normal in meshGeometry.Normals)
                {
                    normals.Add(m_vectorCreator.Create(normal));
                }

                var map = new Dictionary<int, List<IndexedTriangle>>();

                int index0 = 0, index1 = 0, index2 = 0, i = 0;
                foreach (int index in meshGeometry.TriangleIndices)
                {
                    index0 = index1;
                    index1 = index2;
                    index2 = index;
                    if (++i == 3)
                    {
                        var triangle = new IndexedTriangle(index0, index1, index2);
                        triangles.Add(triangle);
                        i = 0;

                        var triangleIndices = new int[] { index0, index1, index2 };
                        foreach (int triangleIndex in triangleIndices)
                        {
                            if (map.ContainsKey(triangleIndex))
                            {
                                var list = map[triangleIndex];
                                list.Add(triangle);
                            }
                            else
                            {
                                map.Add(triangleIndex, new List<IndexedTriangle> { triangle });
                            }
                        }
                    }
                }

                if (!normals.Any())
                {
                    int index = 0;

                    foreach (var vertex in vertices)
                    {
                        if (!map.ContainsKey(index))
                        {
                            ++index;
                            continue;
                        }

                        var list = map[index];

                        var count = 0;
                        var normal = m_vectorCreator.Create();
                        foreach (var triangle in list)
                        {
                            var v1 = vertices[triangle.Index1];
                            var v2 = vertices[triangle.Index2];
                            var v3 = vertices[triangle.Index3];

                            var span1 = m_vectorLogic.Subtract(v1, v2);
                            var span2 = m_vectorLogic.Subtract(v3, v2);
                            var n = m_vectorLogic.GetCrossProduct(span1, span2);

                            // Make sure all contributions point in the same direction
                            if (count != 0 && m_vectorLogic.GetInnerProduct(n, normal) < 0.0)
                            {
                                n.Scale(-1.0);
                            }

                            n.Normalize(); // Makes it a normal and ensures each one the same contribution.
                            normal.X += n.X;
                            normal.Y += n.Y;
                            normal.Z += n.Z;

                            ++count;
                        }

                        Debug.Assert(count != 0);

                        normal.Normalize();

                        normals.Add(normal);

                        index++;
                    }
                }
            }

            return new IndexedTriangleMesh(vertices.ToArray(), normals.ToArray(), triangles.ToArray());
        }

    }
}
