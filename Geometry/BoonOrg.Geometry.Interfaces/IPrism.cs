// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    /// <summary>
    /// A (regular) prism is a 3D mathematical shape span by two opposite triangles.
    /// </summary>
    public interface IPrism : ITriangleContainerProducer, IVolumeProvider
    {
        ITriangle SideA { get; }

        ITriangle SideB { get; }

        void Set(IReadOnlyList<IPoint> coordinates);
    }
}
