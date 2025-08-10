// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.ColorMapping
{
    public interface IColorSet
    {
        string Name { get; set; }

        int Fractions { get; set; }

        bool PaddedFront { get; set; }

        bool PaddedBack { get; set; }

        IColorPin[] Pins { get; set; }
    }
}
