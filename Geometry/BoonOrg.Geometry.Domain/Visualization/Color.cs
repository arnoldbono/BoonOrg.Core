// (c) 2017, 2023 Roland Boon

using System;

using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Domain.Visualization
{
    internal sealed class Color : IColor
    {
        public byte Red { get; set; }

        public byte Green { get; set; }

        public byte Blue { get; set; }

        public byte Alpha { get; set; }

        public Color() : this(0)
        {
        }

        public Color(byte brightness) : this(brightness, brightness, brightness)
        {
        }

        public Color(byte red, byte green, byte blue) : this(red, green, blue, 255)
        {
        }

        public Color(byte red, byte green, byte blue, byte alpha)
        {
            Set(red, green, blue, alpha);
        }

        public void Set(byte red, byte green, byte blue, byte alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public void Set(byte red, byte green, byte blue) => Set(red, green, blue, 255);
    }
}
