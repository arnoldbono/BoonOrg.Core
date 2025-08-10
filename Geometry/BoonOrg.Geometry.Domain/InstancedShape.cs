// (c) 2023 Roland Boon

using System;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    internal class InstancedShape : TriangleContainer, IInstancedShape
    {
        public InstancedShape(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc) : base(identity, boundaryBoxFunc)
        {
        }

        public IInstanceMatrices[] InstanceMatrices { get; set; }

        public ITrimesh Trimesh { get; set; }
    }
}
