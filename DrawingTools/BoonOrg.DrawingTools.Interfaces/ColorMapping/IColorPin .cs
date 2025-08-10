// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry.Visualization;

namespace BoonOrg.DrawingTools.ColorMapping
{
    /// <summary>
    /// A color pinned at a given fraction. The space between two consecutive color pins forms a color gradiant.
    /// </summary>
    public interface IColorPin
    {
        double Fraction { get; set; }

        IColor Color { get; set; }
    }
}
