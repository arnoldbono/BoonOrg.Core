// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry
{
    /// <summary>
    /// By reusing vertices (by equating vertices that are almost equal),
    /// the computed normal for a vertex is no longer per triangle,
    /// but the average of all triangles that share that vertex.
    /// The effect is that the surface is shaded more smoothly.
    /// </summary>
    public interface ISmoothedTrimeshCreator
    {
        ITrimesh Create(ITriangle[] triangles, string name);
    }
}
