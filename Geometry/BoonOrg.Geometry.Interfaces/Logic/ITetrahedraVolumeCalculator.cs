// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    public interface ITetrahedraVolumeCalculator
    {
        /// <summary>
        /// Compute the volume of the space enclosed by the tetrahedra.
        /// </summary>
        /// <remarks>We assume no tetrahedron lies inside another tetrahedron.</remarks>
        /// <param name="tetrahedra">The list of tetrahedra.</param>
        /// <returns>The volume in m^3</returns>
        double Compute(IEnumerable<ITetrahedron> tetrahedra);
    }
}
