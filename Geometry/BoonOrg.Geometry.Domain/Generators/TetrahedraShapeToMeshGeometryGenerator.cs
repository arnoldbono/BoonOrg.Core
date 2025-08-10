// (c) 2017, 2022 Roland Boon

using System;
using System.Linq;
using System.Collections.Generic;
using BoonOrg.Geometry.Generators;

namespace BoonOrg.Geometry.Domain.Generators
{
    internal sealed class TetrahedraShapeToMeshGeometryGenerator : IMeshGeometryGenerator<ICompoundShape<ITetrahedron>>
    {
        private readonly IBasicMeshGeometryGenerator m_meshGeometryGenerator;

        public TetrahedraShapeToMeshGeometryGenerator(IBasicMeshGeometryGenerator meshGeometryGenerator)
        {
            m_meshGeometryGenerator = meshGeometryGenerator;
        }

        public IEnumerable<IMeshGeometry> Generate(IGeometry geometry)
        {
            return Generate((ICompoundShape<ITetrahedron>)geometry);
        }

        public IEnumerable<IMeshGeometry> Generate(ICompoundShape<ITetrahedron> shape)
        {
            m_meshGeometryGenerator.Reset();

            foreach (ITetrahedron t in shape.Containers)
            {
                int index1 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(t.Vertex1);
                int index2 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(t.Vertex2);
                int index3 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(t.Vertex3);
                int index4 = m_meshGeometryGenerator.AddOrReuseNodeGetIndex(t.Vertex4);

                m_meshGeometryGenerator.AddTriangle(index1, index2, index4);
                m_meshGeometryGenerator.AddTriangle(index4, index2, index3);
                m_meshGeometryGenerator.AddTriangle(index1, index4, index3);
                m_meshGeometryGenerator.AddTriangle(index1, index3, index2);
            }

            var indices = m_meshGeometryGenerator.Mesh.TriangleIndices.ToArray();
            int count = indices.Length / 3;
            var trianglesToRemove = new List<int>();
            var indices1 = new int[3] { 0, 0, 0 };
            var indices2 = new int[3] { 0, 0, 0 };
            var ii = 0;
            for (int i = 0; i < count; ++i)
            {
                indices1[0] = indices[ii++];
                indices1[1] = indices[ii++];
                indices1[2] = indices[ii++];

                int jj = ii;
                for (int j = i + 1; j < count; ++j)
                {
                    indices2[0] = indices[jj++];
                    indices2[1] = indices[jj++];
                    indices2[2] = indices[jj++];

                    if (indices1.All(x => indices2.Contains(x)))
                    {
                        if (!trianglesToRemove.Contains(i))
                        {
                            trianglesToRemove.Add(i);
                        }

                        if (!trianglesToRemove.Contains(j))
                        {
                            trianglesToRemove.Add(j);
                        }
                    }
                }
            }

            m_meshGeometryGenerator.ClearTriangleIndices();

            ii = 0;
            for (int i = 0; i < count; ++i)
            {
                if (trianglesToRemove.Contains(i))
                {
                    ii += 3;
                    continue;
                }

                m_meshGeometryGenerator.AddTriangle(indices[ii++], indices[ii++], indices[ii++]);
            }

            return new List<IMeshGeometry> { m_meshGeometryGenerator.Mesh };
        }



        public bool HandlesType(Type type)
        {
            return typeof(ICompoundShape<ITetrahedron>).IsAssignableFrom(type);
        }
    }
}
