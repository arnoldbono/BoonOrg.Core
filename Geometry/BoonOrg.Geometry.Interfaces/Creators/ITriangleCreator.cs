// (c) 2017 Roland Boon

using System.Collections.Generic;

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface ITriangleCreator : ICreator<ITriangle>
    {
        ITriangle Create(IPoint v1, IPoint v2, IPoint v3);

        ITriangle Create(IReadOnlyList<IPoint> vertices);
    }
}
