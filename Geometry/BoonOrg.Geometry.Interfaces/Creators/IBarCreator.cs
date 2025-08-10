// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IBarCreator : ICreator<IBar>
    {
        IBar Create(IPoint center, double width, double length, double height);
    }
}
