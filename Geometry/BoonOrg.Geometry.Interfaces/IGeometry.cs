// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    /// <summary>
    /// A geometry is an item that has a bounding box.
    /// </summary>
    public interface IGeometry : IGeometryItem, IBoundingBoxProvider
    {
    }
}
