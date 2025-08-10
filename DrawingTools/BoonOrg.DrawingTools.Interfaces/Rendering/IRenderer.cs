// (c) 2023 Roland Boon

using System;

using BoonOrg.DrawingTools.Textures;
using BoonOrg.Geometry;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.DrawingTools.Rendering
{
    public interface IRenderer
    {
        void Initialize(IView view, IGeometry geometry);

        IView View { get; }

        IGeometry Geometry { get; }

        void Update(IBitmapTextureSkyHook bitmapTextureSkyHook);

        void Close();

        void Render();

        void Render(IColor color);

        bool Updating { get; }

        bool Updated { get; }

        IRendererUpdateRequest UpdateRequest { get; set; }
    }
}
