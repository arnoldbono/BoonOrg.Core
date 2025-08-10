// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public interface ITetrahedron : ITriangleContainer, IVolumeProvider
    {
        IPoint Vertex1 { get; set; }

        IPoint Vertex2 { get; set; }

        IPoint Vertex3 { get; set; }

        IPoint Vertex4 { get; set; }

        void Set(IReadOnlyList<IPoint> coordinates);
    }
}
