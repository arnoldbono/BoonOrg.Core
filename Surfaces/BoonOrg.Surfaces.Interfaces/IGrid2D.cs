using System;

using BoonOrg.Geometry;

namespace BoonOrg.Surfaces
{
    /// <summary>
    /// A rectangle with its center at <code>Center</code> with the normal <code>Normal</code>.
    /// </summary>
    public interface IGrid2D : IGeometry
    {
        IVector Center { get; }

        IVector Normal { get; }

        double LengthSide1 { get; }

        double LengthSide2 { get; }

        int VertexCountSide1 { get; }

        int VertexCountSide2 { get; }
    }
}
