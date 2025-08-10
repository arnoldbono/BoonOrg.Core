// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface ICylinder : ITriangleContainerProducer
    {
        double RadiusX { get; }

        double RadiusY { get; }

        double Height { get; }

        int Resolution { get; }

        //  First 3 parameters concern the cylinder's size, last parameter determines the cylinder's smoothness.
        void Set(double radiusX, double radiusY, double height, int resolution);
    }
}
