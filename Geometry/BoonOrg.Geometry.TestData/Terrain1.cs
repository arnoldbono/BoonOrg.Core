// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Geometry.TestData
{
    public class Terrain1 : ITerrain
    {
        // The function that defines the surface we are drawing.
        public double F(double x, double y)
        {
            double r2 = x * x + y * y;
            return 8.0 * Math.Cos(r2 / 2.0) / (2.0 + r2);
        }
    }
}
