// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    public interface ITetrahedraFinder
    {
        IEnumerable<ITetrahedron> ComputeTetrahedraInDeformedCell(IPoint[] nodes, IPlane plane);

        IEnumerable<ITetrahedron> ComputeTetrahedraUnderTriangle(ITriangle triangle, IPlane plane);

        IEnumerable<ITetrahedron> ComputeTetrahedraForTriangleWithThreeNodesAboveDepth(List<IPoint> nodes, IPlane plane);

        IEnumerable<ITetrahedron> ComputeTetrahedraForTriangleWithTwoNodesAboveDepth(List<IPoint> nodes, IPlane plane);

        IEnumerable<ITetrahedron> ComputeTetrahedraForTriangleWithOneNodeAboveDepth(List<IPoint> nodes, IPlane plane);

        IEnumerable<ITetrahedron> ComputeTetrahedraForPrism(IPoint v1, IPoint v2, IPoint v3,
            IPoint v4, IPoint v5, IPoint v6);
    }
}
