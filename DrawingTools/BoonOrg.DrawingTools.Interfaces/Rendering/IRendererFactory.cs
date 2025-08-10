// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;

namespace BoonOrg.DrawingTools.Rendering
{
    /// <summary>
    /// The job of this factory class is to produce a renderer for a given geometry
    /// </summary>
    public interface IRendererFactory
    {
        IRenderer Create(IGeometry geometry, IView view);
    }
}
