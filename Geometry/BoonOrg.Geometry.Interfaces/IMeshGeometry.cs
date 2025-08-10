// (c) 2022 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    /// <summary>
    /// An interface to convert to a MeshGeometry3D later (in the case of WPF).
    /// </summary>
    public interface IMeshGeometry
    {
        IEnumerable<IPoint> Positions { get; }

        IEnumerable<IVector> Normals { get; }

        IEnumerable<int> TriangleIndices { get; }

        IEnumerable<IPoint> TextureCoordinates { get; }

        int PositionCount { get; }

        int NormalCount { get; }

        int TriangleCount { get; }
    }
}
