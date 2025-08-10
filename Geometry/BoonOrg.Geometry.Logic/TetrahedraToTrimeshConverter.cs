// (c) 2017 Roland Boon

using System.Collections.Generic;
using System.Linq;
using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class TetrahedraToTrimeshConverter : ITetrahedraToTrimeshConverter
    {
        private ITrimeshCreator m_trimeshCreator;

        public TetrahedraToTrimeshConverter(ITrimeshCreator trimeshCreator)
        {
            m_trimeshCreator = trimeshCreator;
        }

        public ITrimesh GetTopSurface(IEnumerable<ITetrahedron> tetrahedra, string name)
        {
            var triangles = new List<ITriangle>();
            foreach (ITetrahedron tetrahedron in tetrahedra)
            {
                var tetrahedronTriangles = tetrahedron.Triangles.ToList();
                foreach (ITriangle triangle in tetrahedronTriangles)
                {
                    if (IsHighestTriangle(tetrahedronTriangles, triangle))
                    {
                        triangles.Add(triangle);
                    }
                }
            }
            ITrimesh trimesh = m_trimeshCreator.Create(new List<ITriangle>(), name);
            var triangleCompareList = new List<ITriangle>(triangles);
            foreach (ITriangle triangle in triangles)
            {
                if (IsHighestTriangle(triangleCompareList, triangle))
                {
                    trimesh.AddTriangle(triangle);
                }
                else
                {
                    triangleCompareList.Remove(triangle);
                }
            }
            return trimesh;
        }

        private bool IsHighestTriangle(IEnumerable<ITriangle> triangles, ITriangle triangle)
        {
            // Simple version using shared vertices
            foreach (ITriangle t in triangles)
            {
                foreach (IPoint c1 in t.Vertices)
                {
                    foreach (IPoint c2 in triangle.Vertices)
                    {
                        if (c1.X == c2.X && c1.Y == c2.Y)
                        {
                            if (c2.Z < c1.Z)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;

            // Harder, more general version:
            // https://stackoverflow.com/questions/25512037/how-to-determine-if-a-point-lies-over-a-triangle-in-3d
        }
    }
}
