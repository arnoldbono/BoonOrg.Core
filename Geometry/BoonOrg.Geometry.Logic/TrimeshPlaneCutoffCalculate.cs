// (c) 2017, 2023 Roland Boon

using System;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class TrimeshPlaneCutoffCalculate : ITrimeshPlaneCutoffCalculator
    {
        private readonly IIntersectionCalculator m_intersectionCalculator;
        private readonly ILayerGridToTrimeshTransformer m_transformer;
        private readonly ITriangleCreator m_triangleCreator;
        private readonly IVectorCreator m_vectorCreator;
        private readonly IVectorLogic m_vectorLogic;
        private readonly ISmoothedTrimeshCreator m_smoothedTrimeshCreator;

        public TrimeshPlaneCutoffCalculate(IIntersectionCalculator intersectionCalculator,
            ILayerGridToTrimeshTransformer transformer,
            ITriangleCreator triangleCreator,
            IVectorCreator vectorCreator,
            IVectorLogic vectorLogic,
            ISmoothedTrimeshCreator smoothedTrimeshCreator)
        {
            m_intersectionCalculator = intersectionCalculator;
            m_transformer = transformer;
            m_triangleCreator = triangleCreator;
            m_vectorCreator = vectorCreator;
            m_vectorLogic = vectorLogic;
            m_smoothedTrimeshCreator = smoothedTrimeshCreator;
        }

        public ITrimesh CalculateCutoff(ITrimesh trimesh, double depth, string name)
        {
            var intersections = new List<ITriangle>();

            foreach (var triangle in trimesh.Triangles)
            {
                intersections.AddRange(CalculateCutoff(triangle, depth));
            }

            OrientTriangles(intersections, m_vectorCreator.Create(0.0, 0.0, 1.0));

            return m_smoothedTrimeshCreator.Create(intersections.ToArray(), name);
        }

        public ITrimesh CalculateCutoff(ITrimesh trimesh, IPlane plane, string name)
        {
            var intersections = new List<ITriangle>();

            foreach (var triangle in trimesh.Triangles)
            {
                intersections.AddRange(CalculateCutoff(triangle, plane));
            }

            OrientTriangles(intersections, plane.Normal);

            return m_smoothedTrimeshCreator.Create(intersections.ToArray(), name);
        }

        private void OrientTriangles(IEnumerable<ITriangle> triangles, IVector direction)
        {
            foreach (var triangle in triangles)
            {
                if (m_vectorLogic.GetInnerProduct(triangle.ComputeNormal(), direction) < 0.0)
                {
                    triangle.Flip();
                }
            }
        }

        public IEnumerable<ITriangle> CalculateCutoff(ITriangle triangle, IPlane plane)
        {
            IPoint[] vertices = triangle.Vertices.ToArray();
            int aboveCount = m_intersectionCalculator.ComputeCoordinatesAbovePlane(vertices, plane);

            if (aboveCount == 0)
            {
                return Enumerable.Empty<ITriangle>();
            }

            IPoint peak = GetPeak(vertices, (c) => plane.GetTranslatedInnerProductWithNormal(c));
            var nodes = RotateToSetFirstCoordinate(vertices, peak);

            switch (aboveCount)
            {
                case 1:
                    return ComputeCutoffForTriangleWithOneNodeAbovePlane(nodes, plane);
                case 2:
                    return ComputeCutoffForTriangleWithTwoNodesAbovePlane(nodes, plane);
                default:
                case 3:
                    return new List<ITriangle> { triangle };
            }
        }

        public IEnumerable<ITriangle> CalculateCutoff(ITriangle triangle, double depth)
        {
            IPoint[] vertices = triangle.Vertices.ToArray();
            int aboveCount = m_intersectionCalculator.ComputeCoordinatesAboveDepth(vertices, depth);

            List<IPoint> nodes = m_transformer.GetPeakOnFirstNode(vertices, aboveCount);

            IEnumerable<ITriangle> intersection;
            switch (aboveCount)
            {
                case 1:
                    intersection = ComputeCutoffForTriangleWithOneNodeAboveDepth(nodes, depth);
                    break;
                case 2:
                    intersection = ComputeCutoffForTriangleWithTwoNodesAboveDepth(nodes, depth);
                    break;
                case 3:
                    intersection = new List<ITriangle> { triangle };
                    break;
                default:
                    intersection = Enumerable.Empty<ITriangle>();
                    break;
            }
            return intersection;
        }

        public IEnumerable<ITriangle> ComputeCutoffForTriangleWithTwoNodesAbovePlane(List<IPoint> nodes, IPlane plane)
        {
            IPoint node0 = nodes[0];
            IPoint node1 = nodes[1];
            IPoint node2 = nodes[2];

            IPoint nodeAbove, nodeBelow;
            if (plane.GetRelation(node2) == RelationToPlane.Above)
            {
                nodeAbove = node2;
                nodeBelow = node1;
            }
            else
            {
                nodeAbove = node1;
                nodeBelow = node2;
            }
            IPoint i0B = m_intersectionCalculator.ComputeIntersectionWithPlane(node0, nodeBelow, plane);
            IPoint iAB = m_intersectionCalculator.ComputeIntersectionWithPlane(nodeAbove, nodeBelow, plane);

            yield return m_triangleCreator.Create(node0, iAB, nodeAbove);
            yield return m_triangleCreator.Create(node0, i0B, iAB);
        }

        public IEnumerable<ITriangle> ComputeCutoffForTriangleWithOneNodeAbovePlane(List<IPoint> nodes, IPlane plane)
        {
            IPoint node0 = nodes[0];
            IPoint node1 = nodes[1];
            IPoint node2 = nodes[2];

            IPoint i01 = m_intersectionCalculator.ComputeIntersectionWithPlane(node0, node1, plane);
            IPoint i02 = m_intersectionCalculator.ComputeIntersectionWithPlane(node0, node2, plane);

            yield return m_triangleCreator.Create(node0, i01, i02);
        }

        public IEnumerable<ITriangle> ComputeCutoffForTriangleWithTwoNodesAboveDepth(List<IPoint> nodes, double depth)
        {
            System.Diagnostics.Debug.Assert(nodes.Count(n => n.Z > -depth) == 2);

            IPoint node0 = nodes[0];
            IPoint node1 = nodes[1];
            IPoint node2 = nodes[2];

            // Pre: node0 is the peak, so it is above depth
            System.Diagnostics.Debug.Assert(node0.Z > -depth);

            if (node2.Z > -depth)
            {
                IPoint i01 = m_intersectionCalculator.ComputeIntersectionAtDepth(node0, node1, depth);
                IPoint i12 = m_intersectionCalculator.ComputeIntersectionAtDepth(node1, node2, depth);

                yield return m_triangleCreator.Create(node0, i01, i12);
                yield return m_triangleCreator.Create(node2, node0, i12);
            }
            else // (node1.Z >= -depth)
            {
                IPoint i02 = m_intersectionCalculator.ComputeIntersectionAtDepth(node0, node2, depth);
                IPoint i12 = m_intersectionCalculator.ComputeIntersectionAtDepth(node1, node2, depth);

                yield return m_triangleCreator.Create(node0, i12, node1);
                yield return m_triangleCreator.Create(node0, i02, i12);
            }
        }

        public IEnumerable<ITriangle> ComputeCutoffForTriangleWithOneNodeAboveDepth(List<IPoint> nodes, double depth)
        {
            IPoint node0 = nodes[0];
            IPoint node1 = nodes[1];
            IPoint node2 = nodes[2];

            IPoint i01 = m_intersectionCalculator.ComputeIntersectionAtDepth(node0, node1, depth);
            IPoint i02 = m_intersectionCalculator.ComputeIntersectionAtDepth(node0, node2, depth);

            yield return m_triangleCreator.Create(node0, i01, i02);
        }

        public IPoint GetPeak(IPoint[] cell, Func<IPoint, double> f)
        {
            IPoint peakNode = null;
            double peakDistance = double.MinValue;
            foreach (IPoint node in cell)
            {
                double distance = f(node);
                if (distance > peakDistance)
                {
                    peakNode = node;
                    peakDistance = distance;
                }
            }
            return peakNode;
        }

        public List<IPoint> RotateToSetFirstCoordinate(IPoint[] cell, IPoint first)
        {
            var nodes = new List<IPoint>(cell);
            int index = nodes.IndexOf(first);
            if (index > 0)
            {
                int count = cell.Length;
                int j = 0;
                for (int i = index; i < count; ++i, ++j)
                {
                    nodes[j] = cell[i];
                }
                for (int i = 0; i < index; ++i, ++j)
                {
                    nodes[j] = cell[i];
                }
            }
            return nodes;
        }

    }
}
