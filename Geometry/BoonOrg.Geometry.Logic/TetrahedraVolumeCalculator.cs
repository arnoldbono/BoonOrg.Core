// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class TetrahedraVolumeCalculator : ITetrahedraVolumeCalculator
    {
        /// <inheritdoc/>
        public double Compute(IEnumerable<ITetrahedron> tetrahedron)
        {
            double rv = 0.0;
            foreach (ITetrahedron t in tetrahedron)
            {
                rv += t.GetVolume();
            }
            return rv;
        }
    }
}
