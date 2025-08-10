// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IEllipsoidCreator : ICreator<IEllipsoid>
    {
        IEllipsoid Create(double radiusX, double radiusY, double height, int resolution);
    }
}
