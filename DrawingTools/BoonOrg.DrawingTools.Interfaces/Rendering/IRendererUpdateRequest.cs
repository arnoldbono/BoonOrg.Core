// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.Rendering
{
    public interface IRendererUpdateRequest
    {
        bool ReplaceRenderer { get; set; }

        bool UpdateVertices { get; set; } // and/or normals

        bool UpdatePropertyValues { get; set; }

        bool UpdateColorSet { get; set; }

        bool UpdateUvs { get; set; }

        bool AttributeChange { get; set; }
    }
}
