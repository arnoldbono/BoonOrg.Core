// (c) 2017 Roland Boon

using System.Linq;
using System.Collections.Generic;
using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    public sealed class TetrahedraFinder : ITetrahedraFinder
    {
        private readonly ITetrahedronCreator m_tetrahedronCreator;
        private readonly IIntersectionCalculator m_intersectionCalculator;
        private readonly ITrimeshPlaneCutoffCalculator m_cutoffFinder;
        private readonly ITriangleCreator m_triangleCreator;

        public TetrahedraFinder(ITetrahedronCreator tetrahedronCreator,
            IIntersectionCalculator intersectionCalculator,
            ITrimeshPlaneCutoffCalculator cutoffFinder,
            ITriangleCreator triangleCreator)
        {
            m_tetrahedronCreator = tetrahedronCreator;
            m_intersectionCalculator = intersectionCalculator;
            m_cutoffFinder = cutoffFinder;
            m_triangleCreator = triangleCreator;
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraUnderTriangle(ITriangle triangle, IPlane plane)
        {
            IPoint[] vertices = triangle.Vertices.ToArray();
            int aboveCount = m_intersectionCalculator.ComputeCoordinatesAbovePlane(vertices, plane);

            IPoint peak = m_cutoffFinder.GetPeak(vertices, (c) => plane.GetTranslatedInnerProductWithNormal(c));
            var nodes = m_cutoffFinder.RotateToSetFirstCoordinate(vertices, peak);

            IEnumerable<ITetrahedron> tetrahedra = null;
            switch (aboveCount)
            {
                case 1:
                    tetrahedra = ComputeTetrahedraForTriangleWithOneNodeAboveDepth(nodes, plane);
                    break;
                case 2:
                    tetrahedra = ComputeTetrahedraForTriangleWithTwoNodesAboveDepth(nodes, plane);
                    break;
                case 3:
                    tetrahedra = ComputeTetrahedraForTriangleWithThreeNodesAboveDepth(nodes, plane);
                    break;
            }
            if (tetrahedra != null)
            {
                foreach (ITetrahedron t in tetrahedra)
                {
                    yield return t;
                }
            }
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraInDeformedCell(IPoint[] cell, IPlane plane)
        {
            IPoint node0 = cell[0];
            IPoint node1 = cell[1];
            IPoint node2 = cell[2];
            IPoint node3 = cell[3];

            var triangles = new List<ITriangle>
            {
                m_triangleCreator.Create(node0, node2, node1),
                m_triangleCreator.Create(node0, node3, node2)
            };

            foreach (ITriangle triangle in triangles)
            {
                IEnumerable<ITetrahedron> tetrahedra = ComputeTetrahedraUnderTriangle(triangle, plane);
                foreach (ITetrahedron t in tetrahedra)
                {
                    yield return t;
                }
            }
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraForTriangleWithThreeNodesAboveDepth(List<IPoint> nodes, IPlane plane)
        {
            IPoint node0 = nodes[0];
            IPoint node1 = nodes[1];
            IPoint node2 = nodes[2];

            var fc0 = m_intersectionCalculator.MapToPlane(node0, plane);
            var fc1 = m_intersectionCalculator.MapToPlane(node1, plane);
            var fc2 = m_intersectionCalculator.MapToPlane(node2, plane);

            return ComputeTetrahedraForPrism(fc0, fc1, fc2, node0, node1, node2);
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraForTriangleWithTwoNodesAboveDepth(List<IPoint> nodes, IPlane plane)
        {
            IPoint node0 = nodes[0];
            IPoint node1 = nodes[1];
            IPoint node2 = nodes[2];

            var fc0 = m_intersectionCalculator.MapToPlane(node0, plane);
            var fc1 = m_intersectionCalculator.MapToPlane(node1, plane);
            var fc2 = m_intersectionCalculator.MapToPlane(node2, plane);

            // Compute the tetrahedrons when the two nodes above depth are at opposite sides.
            IPoint nodeAbove, nodeBelow, fcAbove;
            if (plane.GetRelation(node2) == RelationToPlane.Above)
            {
                nodeAbove = node2;
                nodeBelow = node1;
                fcAbove = fc2;
            }
            else
            {
                nodeAbove = node1;
                nodeBelow = node2;
                fcAbove = fc1;
            }
            IPoint i0B = m_intersectionCalculator.ComputeIntersectionWithPlane(node0, nodeBelow, plane);
            IPoint iAB = m_intersectionCalculator.ComputeIntersectionWithPlane(nodeAbove, nodeBelow, plane);

            return ComputeTetrahedraForPrism(fc0, node0, i0B, fcAbove, nodeAbove, iAB);
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraForTriangleWithOneNodeAboveDepth(List<IPoint> nodes, IPlane plane)
        {
            IPoint node0 = nodes[0];
            IPoint node1 = nodes[1];
            IPoint node2 = nodes[2];

            var fc0 = m_intersectionCalculator.MapToPlane(node0, plane);

            IPoint i01 = m_intersectionCalculator.ComputeIntersectionWithPlane(node0, node1, plane);
            IPoint i02 = m_intersectionCalculator.ComputeIntersectionWithPlane(node0, node2, plane);

            yield return m_tetrahedronCreator.Create(fc0, node0, i01, i02);
        }

        public IEnumerable<ITetrahedron> ComputeTetrahedraForPrism(IPoint v1, IPoint v2, IPoint v3,
            IPoint v4, IPoint v5, IPoint v6)
        {
            ITetrahedron t;
            t = m_tetrahedronCreator.Create(v1, v2, v3, v4);
            if (t.GetVolume() != 0.0)
            {
                yield return t;
            }
            t = m_tetrahedronCreator.Create(v2, v3, v4, v5);
            if (t.GetVolume() != 0.0)
            {
                yield return t;
            }
            t = m_tetrahedronCreator.Create(v3, v5, v6, v4);
            if (t.GetVolume() != 0.0)
            {
                yield return t;
            }
        }
    }
}
