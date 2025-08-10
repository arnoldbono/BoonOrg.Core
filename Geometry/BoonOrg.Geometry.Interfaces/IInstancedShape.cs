// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry
{
    public interface IInstancedShape : ITriangleContainer
    {
        IInstanceMatrices[] InstanceMatrices { get; set; }

        ITrimesh Trimesh { get; set; }
    }
}
