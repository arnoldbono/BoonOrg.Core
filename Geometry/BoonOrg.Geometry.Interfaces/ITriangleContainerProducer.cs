// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface ITriangleContainerProducer : ISurface
    {
        ITriangleContainer Create();
    }
}
