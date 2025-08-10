// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
     internal sealed class TrimeshPlaneIntersectionToTetrahedra : IPlaneIntersectionToTetrahedra<ITrimesh>
     {
        private readonly ITetrahedraFinder m_finder;

        public TrimeshPlaneIntersectionToTetrahedra(ITetrahedraFinder finder)
        {
            m_finder = finder;
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraInEnclosedVolume(ISurface surface, IPlane plane)
        {
            return ComputeTetrahedraInEnclosedVolume(surface as ITrimesh, plane);
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraInEnclosedVolume(ITrimesh trimesh, IPlane plane)
        {
            foreach (ITriangle triangle in trimesh.Triangles)
            {
                IEnumerable<ITetrahedron> tetrahedra = m_finder.ComputeTetrahedraUnderTriangle(triangle, plane);
                if (tetrahedra == null)
                {
                    continue;
                }

                foreach (ITetrahedron t in tetrahedra)
                {
                    yield return t;
                }
            }
        }
    }
}
