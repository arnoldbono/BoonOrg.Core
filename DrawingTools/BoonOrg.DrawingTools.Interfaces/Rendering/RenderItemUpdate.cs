// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.Rendering
{
    public class RendererUpdateRequest : IRendererUpdateRequest
    {
        public bool ReplaceRenderer { get; set; } = false;

        public bool UpdateVertices { get; set; } = true;

        public bool UpdatePropertyValues { get; set; } = true;

        public bool UpdateColorSet { get; set; } = true;

        public bool UpdateUvs { get; set; } = true;

        public bool AttributeChange { get; set; } = true;
    }
}
