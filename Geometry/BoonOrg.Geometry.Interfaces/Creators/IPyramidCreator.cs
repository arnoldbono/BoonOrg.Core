// (c) 2017 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface IPyramidCreator : ICreator<IPyramid>
    {
        IPyramid Create(double size);

        IPyramid Create(double width, double length, double height);
    }
}
