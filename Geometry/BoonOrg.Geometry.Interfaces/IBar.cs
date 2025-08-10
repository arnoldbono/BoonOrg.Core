// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IBar : ITriangleContainerProducer
    {
        IPoint Center { get; }

        double Width { get; }

        double Length { get; }

        double Height { get; }

        // First parameters is the bar center, the last 3 parameters are the bar size at each axis
        void Set(IPoint center, double width, double length, double height);
    }
}
