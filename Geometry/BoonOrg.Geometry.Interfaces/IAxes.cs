// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IAxes : ICompoundShape<ITriangleContainer>
    {
        IVector Lengths { get; }

        IPoint Center { get; }

        void SetAxes(IPoint center, IVector lengths);

        void SetAxes(IBoundingBox box);

        void SetAxes(double margin, IBoundingBox box);
    }
}
