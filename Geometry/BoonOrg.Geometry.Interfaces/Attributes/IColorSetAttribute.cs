// (c) 2023 Roland Boon

namespace BoonOrg.Geometry.Attributes
{
    public interface IColorSetAttribute : IGeometryAttribute
    {
        string TextureName { get; set; }

        int Index { get; set; }
    }
}
