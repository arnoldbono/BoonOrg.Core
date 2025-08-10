// (c) 2017 Roland Boon

namespace BoonOrg.Geometry.Domain
{
    internal sealed class Area : IArea
    {
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }

        public Area()
        {
            MinX = -100.0;
            MaxX = 100.0;
            MinY = -100.0;
            MaxY = 100.0;
        }

        public Area(double minx, double maxx, double miny, double maxy)
        {
            MinX = minx;
            MaxX = maxx;
            MinY = miny;
            MaxY = maxy;
        }

        public double ScaleX => MaxX - MinX;
        public double ScaleY => MaxY - MinY;

        public double GetNormalizedX(double x)
        {
            return (x - MinX) / ScaleX;
        }

        public double GetNormalizedY(double y)
        {
            return (y - MinY) / ScaleY;
        }
    }
}
