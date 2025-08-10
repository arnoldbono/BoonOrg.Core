// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface ICylinderCreator : ICreator<ICylinder>
    {
        ICylinder Create(double radiusX, double radiusY, double height, int resolution);
    }
}
