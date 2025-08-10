// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Geometry.TestData
{
    public class Terrain2 : ITerrain
    {
        /// <inheritdoc/>
        public double F(double x, double y)
        {
            const double two_pi = 2.0 * Math.PI;
            double r2 = x * x + y * y;
            double r = Math.Sqrt(r2);
            double theta = Math.Atan2(y, x);
            return Math.Exp(-r2) * Math.Sin(two_pi * r) * Math.Cos(3 * theta);
        }
    }
}
