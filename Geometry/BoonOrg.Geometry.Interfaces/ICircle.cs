// (c) 2020 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    /// <summary>
    /// A circle.
    /// </summary>
    public interface ICircle : IGeometry
    {
        /// <summary>
        /// The circle's center.
        /// </summary>
        IPoint Center { get; }

        /// <summary>
        /// The normal of the circle.
        /// </summary>
        IVector Normal { get; }

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        double Radius { get; set; }

        /// <summary>
        /// The number points to draw the circle.
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// The vertices that draw the circle.
        /// </summary>
        IEnumerable<IPoint> Vertices { get; }

        /// <summary>
        /// Sets the parameters of the circle.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="normal">The normal.</param>
        /// <param name="name">The name of the circle.</param>
        void Set(IPoint center, IVector normal, string name);
    }
}
