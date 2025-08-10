// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry.Visualization;

namespace BoonOrg.DrawingTools.ColorMapping
{
    public sealed class ColorPin : IColorPin
    {
        public ColorPin(double fraction, IColor color)
        {
            Fraction = fraction;
            Color = color;
        }

        public ColorPin() : this(0.0, null)
        {

        }

        public double Fraction { get; set; }

        public IColor Color { get; set; }

        public override string ToString()
        {
            return $@"[{Fraction}, {Color}]";
        }
    }
}
