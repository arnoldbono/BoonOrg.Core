// (c) 2017 Roland Boon

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class VectorCreator : IVectorCreator
    {
        public IVector Create() => new Vector();

        public IVector Create(double x, double y, double z) => new Vector(x, y, z);

        public IVector Create(IPoint p) => new Vector(p.X, p.Y, p.Z);
    }
}
