// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IEllipsoid : ITriangleContainerProducer
    {
        double RadiusX { get; }

        double RadiusY { get; }

        double Height { get; }

        int Resolution { get; }

        void Set(double radiusX, double radiusY, double height, int resolution);
    }
}
