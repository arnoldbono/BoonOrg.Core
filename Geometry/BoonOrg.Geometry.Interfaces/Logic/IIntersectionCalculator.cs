// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    public interface IIntersectionCalculator
    {
        /// <summary>
        /// Compute the intersection between two coordinates with a plane at a given depth, one above and the other below (or on).
        /// </summary>
        /// <param name="above">The cooridinate above the given depth.</param>
        /// <param name="below">The cooridinate below (or on) the given depth</param>
        /// <param name="depth">The depth of the Fluid Contact.</param>
        /// <returns>The coordinate of the intersection point.</returns>
        IPoint ComputeIntersectionAtDepth(IPoint above, IPoint below, double depth);

        IPoint ComputeIntersectionWithPlane(IPoint above, IPoint below, IPlane plane);

        int ComputeCoordinatesAboveDepth(IEnumerable<IPoint> coordinates, double depth);

        int ComputeCoordinatesBelowDepth(IEnumerable<IPoint> coordinates, double depth);

        int ComputeCoordinatesAbovePlane(IEnumerable<IPoint> coordinates, IPlane plane);

        int ComputeCoordinatesBelowPlane(IEnumerable<IPoint> coordinates, IPlane plane);

        IPoint MapToDepth(IPoint coordinate, double depth);

        IPoint MapToPlane(IPoint coordinate, IPlane plane);
    }
}
