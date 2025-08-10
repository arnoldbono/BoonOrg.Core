// (c) 2019, 2023 Roland Boon

namespace BoonOrg.Geometry.Attributes
{
    public interface IDrawSurfaceAttribute : IGeometryAttribute
    {
        bool ShowSurface { get; set; }

        bool ShowWireframe { get; set; }

        bool ShowProperty { get; set; }

        bool UseLighting { get; set; }

        // If set, each triangle vertex gets the same normal. As a consequence, vertices cannot be shared by triangles.
        bool FacetedTriangles { get; set; }

        // Index in the 'Properties' array of the mesh.
        int SelectedProperty { get; set; }

        int SelectedColorMap { get; set; }
    }
}
