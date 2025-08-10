// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    public interface IPlaneIntersectionToTetrahedra<T> : IPlaneIntersectionToTetrahedra where T : ISurface
    {
        IEnumerable<ITetrahedron> ComputeTetrahedraInEnclosedVolume(T surface, IPlane plane);
    }

    public interface IPlaneIntersectionToTetrahedra
    {
        IEnumerable<ITetrahedron> ComputeTetrahedraInEnclosedVolume(ISurface surface, IPlane plane);
    }
}
