// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    public interface ITetrahedraToTrimeshConverter
    {
        ITrimesh GetTopSurface(IEnumerable<ITetrahedron> tetrahedra, string name);
    }
}
