// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;

namespace BoonOrg.DrawingTools.Rendering
{
    public sealed class RenderItem
    {
        public IGeometry Geometry { get; }

        public IRenderer Renderer { get; }

        public RenderItem(IGeometry geometry, IRenderer renderer)
        {
            Geometry = geometry;
            Renderer = renderer;
        }
    }
}
