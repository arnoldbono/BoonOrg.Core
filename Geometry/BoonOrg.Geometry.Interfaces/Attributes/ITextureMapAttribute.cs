// (c) 2024 Roland Boon

namespace BoonOrg.Geometry.Attributes
{
    public interface ITextureMapAttribute : IGeometryAttribute
    {
        bool Show { get; set; }

        string TextureMap { get; set; }
    }
}
