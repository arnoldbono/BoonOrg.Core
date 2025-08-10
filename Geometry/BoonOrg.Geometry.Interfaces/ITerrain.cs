// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface ITerrain
    {
        /// <summary>
        /// The function that defines the surface we are drawing.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        double F(double x, double y);
    }
}
