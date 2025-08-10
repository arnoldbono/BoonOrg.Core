// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IVectorCreator : ICreator<IVector>
    {
        IVector Create(double x, double y, double z);

        IVector Create(IPoint p);
    }
}
