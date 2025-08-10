// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IPlaneCreator : ICreator<IPlane>
    {
        IPlane Create(IPoint center, IVector normal, string name);
    }
}
