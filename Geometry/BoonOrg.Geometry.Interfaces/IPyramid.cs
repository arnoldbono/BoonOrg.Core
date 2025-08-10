// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IPyramid : ITriangleContainerProducer
    {
        double Width { get; }

        double Length { get; }

        double Height { get; }

        void Set(double size);

        void Set(double width, double length, double height);
    }
}
