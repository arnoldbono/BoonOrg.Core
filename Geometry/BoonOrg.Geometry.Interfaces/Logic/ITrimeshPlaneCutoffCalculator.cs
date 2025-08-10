// (c) 2017 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    public interface ITrimeshPlaneCutoffCalculator
    {
        ITrimesh CalculateCutoff(ITrimesh trimesh, double depth, string name);

        ITrimesh CalculateCutoff(ITrimesh trimesh, IPlane plane, string name);

        IEnumerable<ITriangle> CalculateCutoff(ITriangle triangle, double depth);

        IEnumerable<ITriangle> ComputeCutoffForTriangleWithTwoNodesAboveDepth(List<IPoint> nodes, double depth);

        IEnumerable<ITriangle> ComputeCutoffForTriangleWithOneNodeAboveDepth(List<IPoint> nodes, double depth);

        IPoint GetPeak(IPoint[] cell, Func<IPoint, double> f);

        List<IPoint> RotateToSetFirstCoordinate(IPoint[] cell, IPoint first);
    }
}
