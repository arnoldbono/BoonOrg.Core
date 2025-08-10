// (c) 2018 Roland Boon

namespace BoonOrg.Geometry.Attributes
{
    public interface IPropertyAttribute : IGeometryAttribute
    {
        IPropertyType PropertyType { get; set; }
    }
}
