// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
     internal sealed class LayerGridPlaneIntersectionToTetrahedra : IPlaneIntersectionToTetrahedra<ILayerGrid>
     {
        private readonly ITetrahedraFinder m_finder;

        public LayerGridPlaneIntersectionToTetrahedra(ITetrahedraFinder helper)
        {
            m_finder = helper;
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraInEnclosedVolume(ISurface surface, IPlane plane)
        {
            return ComputeTetrahedraInEnclosedVolume(surface as ILayerGrid, plane);
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraInEnclosedVolume(ILayerGrid grid, IPlane plane)
        {
            foreach (IPoint[] cell in grid.Cells)
            {
                IEnumerable<ITetrahedron> tetrahedra = m_finder.ComputeTetrahedraInDeformedCell(cell, plane);

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
