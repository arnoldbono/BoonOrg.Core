// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.ColorMapping
{
    public sealed class ColorSet : IColorSet
    {
        public string Name { get; set; }

        public int Fractions { get; set; }

        public IColorPin[] Pins { get; set; }

        public bool PaddedFront { get; set; }

        public bool PaddedBack { get; set; }

        public override string ToString()
        {
            return $@"[{Name}, {Fractions}]";
        }
    }
}
