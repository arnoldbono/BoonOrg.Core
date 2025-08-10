// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Identification;

namespace BoonOrg.DrawingTools.Rendering
{
    public interface IView : IIdentifiable
    {
        void Initialize(float width, float height);

        IBoundingBox BoundingBox { get; set; }

        ICamera Camera { get; }

        IViewport Viewport { get; }

        IRenderItemContainer RenderItems { get; }

        IGeometryAttributeContainer Attributes { get; }
    }
}
