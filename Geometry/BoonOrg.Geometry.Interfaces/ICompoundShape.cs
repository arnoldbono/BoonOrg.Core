// (c) 2015, 2016, 2017, 2018 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public interface ICompoundShape<T> : ISurface where T : ITriangleContainer
    {
        IEnumerable<T> Containers { get; }

        void Add(IEnumerable<T> containers);
    }
}
