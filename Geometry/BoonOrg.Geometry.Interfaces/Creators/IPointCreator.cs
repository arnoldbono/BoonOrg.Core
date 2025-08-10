// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IPointCreator : ICreator<IPoint>
    {
        IPoint Create(double x, double y, double z);

        IPoint Create(double[] p);
    }
}
