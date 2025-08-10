// (c) 2017 Roland Boon

using System.Collections.Generic;

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface ICompoundShapeCreator<T> : ICreator<ICompoundShape<T>> where T : ITriangleContainer
    {
        ICompoundShape<T> Create(IEnumerable<T> containers, string name);
    }
}
