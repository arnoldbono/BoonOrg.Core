// (c) 2017 Roland Boon

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class AreaCreator : IAreaCreator
    {
        public IArea Create() => new Area();

        public IArea Create(double minX, double maxX, double minY, double maxY) =>
            new Area(minX, maxX, minY, maxY);
    }
}
