// (c) 2023 Roland Boon

using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Domain.Visualization
{
    public class PbrProperty : IPbrProperty
    {
        public IColour Colour { get; set; } = new Colour( 1.0f, 0.766f, 0.336f, 1.0f ); // Gold like color

        public float MetallicRatio { get; set; } = 1.0f;

        public float RoughnessRatio { get; set; } = 0.0f;
    }
}
