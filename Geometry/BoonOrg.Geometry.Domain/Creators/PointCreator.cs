// (c) 2017 Roland Boon

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class PointCreator : IPointCreator
    {
        public IPoint Create() => new Point();

        public IPoint Create(double x, double y, double z) => new Point(x, y, z);

        public IPoint Create(double[] p) => new Point(p);
    }
}
