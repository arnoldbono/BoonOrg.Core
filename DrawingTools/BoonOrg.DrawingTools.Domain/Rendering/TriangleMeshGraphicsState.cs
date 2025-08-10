// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.Rendering
{
    internal sealed class TriangleMeshGraphicsState : ITriangleMeshGraphicsState
    {
        public ShadeMode ShadeMode { get; set; } = ShadeMode.Smooth;

        public TriangleMeshDrawTechnique DrawTechnique { get; set; } = TriangleMeshDrawTechnique.ColorSet;
    }
}
