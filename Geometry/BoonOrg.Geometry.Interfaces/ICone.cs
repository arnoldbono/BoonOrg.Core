// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface ICone : ITriangleContainerProducer
    {
        double RadiusX { get; }

        double RadiusY { get; }

        double Height { get; }

        int Resolution { get; }

        /// <summary>
        /// Set dimensions.
        /// </summary>
        /// <param name="radiusX">Cone bottom ellipse long axis length.</param>
        /// <param name="radiusY">Cone bottom ellipse short axis length.</param>
        /// <param name="height">Cone height.</param>
        /// <param name="resolution">Cone resolution (smoothness).</param>
        void Set(double radiusX, double radiusY, double height, int resolution);
    }
}
