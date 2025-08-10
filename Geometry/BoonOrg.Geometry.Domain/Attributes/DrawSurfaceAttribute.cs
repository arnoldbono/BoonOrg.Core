// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Domain.Attributes
{
    public class DrawSurfaceAttribute : IDrawSurfaceAttribute
    {
        public DrawSurfaceAttribute()
        {
            ShowSurface = false;
            ShowWireframe = false;
            ShowProperty = false;
            UseLighting = true;
            FacetedTriangles = false;
            SelectedProperty = 0;
            SelectedColorMap = -1; // No color selected
        }

        public bool ShowSurface { get; set; }

        public bool ShowWireframe { get; set; }

        public bool ShowProperty { get; set; }

        public bool UseLighting { get; set; }

        public bool FacetedTriangles { get; set; }

        public int SelectedProperty { get; set; }

        public int SelectedColorMap { get; set; }
    }
}
