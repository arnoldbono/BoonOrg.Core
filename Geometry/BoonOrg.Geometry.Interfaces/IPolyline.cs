// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public interface IPolyline : IBoundingBoxProvider
    {
        IEnumerable<IPoint> Vertices { get; }

        bool Closed { get; }

        void Clear();

        void Append(IPoint point);

        void Insert(int index, IPoint point);

        void Remove(IPoint point);

        void Set(IEnumerable<IPoint> points, bool closed);
    }
}
