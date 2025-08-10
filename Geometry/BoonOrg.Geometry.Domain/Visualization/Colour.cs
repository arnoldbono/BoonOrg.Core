// (c) 2023 Roland Boon

using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Domain.Visualization
{
    internal sealed class Colour : IColour
    {
        public float Red { get; set; }

        public float Green { get; set; }

        public float Blue { get; set; }

        public float Alpha { get; set; }

        public Colour() : this(0.0f)
        {
        }

        public Colour(float brightness) : this(brightness, brightness, brightness)
        {
        }

        public Colour(float red, float green, float blue) : this(red, green, blue, 1.0f)
        {
        }

        public Colour(float red, float green, float blue, float alpha)
        {
            Set(red, green, blue, alpha);
        }

        public void Set(float red, float green, float blue, float alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public void Set(float red, float green, float blue) => Set(red, green, blue, 1.0f);
    }
}
