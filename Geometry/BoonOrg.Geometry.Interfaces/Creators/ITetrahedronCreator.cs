// (c) 2017 Roland Boon

using System.Collections.Generic;

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface ITetrahedronCreator : ICreator<ITetrahedron>
    {
        ITetrahedron Create(IPoint v1, IPoint v2, IPoint v3, IPoint v4);

        ITetrahedron Create(IReadOnlyList<IPoint> vertices);
    }
}
