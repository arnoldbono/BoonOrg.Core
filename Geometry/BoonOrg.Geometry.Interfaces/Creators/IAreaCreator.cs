// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IAreaCreator : ICreator<IArea>
    {
        IArea Create(double minX, double maxX, double minY, double maxY);
    }
}
