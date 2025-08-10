// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class PlaneIntersectionVolumeCalculator : IPlaneIntersectionVolumeCalculator
    {
        private readonly IPlaneIntersectionToTetrahedra<ITrimesh> m_tetrahedraFinder;
        private readonly ITetrahedraVolumeCalculator m_volumeCalculator;

        public PlaneIntersectionVolumeCalculator(IPlaneIntersectionToTetrahedra<ITrimesh> tetrahedraFinder,
            ITetrahedraVolumeCalculator volumeCalculator)
        {
            m_tetrahedraFinder = tetrahedraFinder;
            m_volumeCalculator = volumeCalculator;
        }

        /// <inheritdoc/>
        public double Compute(ITrimesh trimesh, IPlane plane)
        {
            IEnumerable<ITetrahedron> tetrahedra =
                m_tetrahedraFinder.ComputeTetrahedraInEnclosedVolume(trimesh, plane);
            return m_volumeCalculator.Compute(tetrahedra);
        }
    }
}
