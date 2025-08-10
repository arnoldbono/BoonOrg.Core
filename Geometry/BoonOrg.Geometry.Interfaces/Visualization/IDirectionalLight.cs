// (c) 2017 Roland Boon

namespace BoonOrg.Geometry.Visualization
{
    public interface IDirectionalLight : ILight
    {
        IVector Direction { get; set; }
    }
}
