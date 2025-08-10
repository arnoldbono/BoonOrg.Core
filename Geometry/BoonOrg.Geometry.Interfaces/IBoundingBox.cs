// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public interface IBoundingBox : IVolumeProvider
    {
        double MinX { get; }
        double MaxX { get; }
        double MinY { get; }
        double MaxY { get; }
        double MinZ { get; }
        double MaxZ { get; }

        double ScaleX { get; }
        double ScaleY { get; }
        double ScaleZ { get; }

        double Scale { get; }

        // Get a copy of the center point.
        IPoint Center { get; }

        IArea GetArea();

        void Expand(IPoint p);

        void Expand(IBoundingBox box);

        void Expand(IEnumerable<IPoint> vertices);
    }
}
