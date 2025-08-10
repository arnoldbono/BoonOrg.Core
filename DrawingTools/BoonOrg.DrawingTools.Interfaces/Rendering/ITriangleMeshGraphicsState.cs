// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.DrawingTools.Rendering
{
    public enum TriangleMeshDrawTechnique
    {
        ColorPerVertex,
        ColorPerTriangle,
        ColorSet,
        VertexPointer
    }

    public enum ShadeMode
    {
        Flat,
        Smooth
    }

    public interface ITriangleMeshGraphicsState : IGeometryAttribute
    {
        ShadeMode ShadeMode { get; set; }

        TriangleMeshDrawTechnique DrawTechnique { get; set; }
    }
}
