// (c) 2017, 2023 Roland Boon

namespace BoonOrg.Geometry.Visualization
{
    public interface IColor
    {
        byte Red { get; set; }

        byte Green { get; set; }

        byte Blue { get; set; }

        byte Alpha { get; set; }

        void Set(byte red, byte green, byte blue, byte alpha);

        void Set(byte red, byte green, byte blue);
    }
}
