// (c) 2023 Roland Boon

namespace BoonOrg.Geometry.Visualization
{
    public interface IPbrProperty
    {
        IColour Colour { get; set; }

        float MetallicRatio { get; set; } // [0..1]

        float RoughnessRatio { get; set; } // [0..1]
    }
}
