// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IConeCreator : ICreator<ICone>
    {
        ICone Create(double radiusX, double radiusY, double height, int resolution);
    }
}
