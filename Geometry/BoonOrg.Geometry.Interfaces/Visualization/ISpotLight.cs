// (c) 2017 Roland Boon

namespace BoonOrg.Geometry.Visualization
{
    public interface ISpotLight : ILight
    {
        IPoint Location { get; set; }

        IVector Direction { get; set; }

        double InnerCone { get; set; }

        double OuterCone { get; set; }
    }
}
