// (c) 2017 Roland Boon

using System.Collections.Generic;

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IPrismCreator : ICreator<IPrism>
    {
        IPrism Create(ITriangle sideA, ITriangle sideB);

        IPrism Create(IPoint v1, IPoint v2, IPoint v3, IPoint v4, IPoint v5, IPoint v6);

        IPrism Create(IReadOnlyList<IPoint> vertices);
    }
}
