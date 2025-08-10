// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IArea
    {
        double MinX { get; set; }

        double MaxX { get; set; }

        double MinY { get; set; }

        double MaxY { get; set; }

        double ScaleX { get; }

        double ScaleY { get; }

        double GetNormalizedX(double x);

        double GetNormalizedY(double y);
    }
}
