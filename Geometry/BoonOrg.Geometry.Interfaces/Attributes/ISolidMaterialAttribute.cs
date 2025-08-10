// (c) 2019 Roland Boon

using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Attributes
{
    public interface ISolidMaterialAttribute : IGeometryAttribute
    {
        IColor Front { get; set; }

        IColor Back { get; set; }
    }
}
