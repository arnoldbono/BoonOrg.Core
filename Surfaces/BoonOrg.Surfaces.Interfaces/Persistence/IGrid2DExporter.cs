// (c) 2024 Roland Boon

using System;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Surfaces.Persistence
{
    public interface IGrid2DExporter
    {
        string Extension { get; }

        bool Export(IGrid2D[] surfaces, IMaterial[] materials, string path, bool includeNormals);
    }
}
