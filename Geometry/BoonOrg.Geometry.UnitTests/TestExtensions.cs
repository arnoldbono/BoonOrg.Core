// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Geometry.UnitTests
{
    public static class TestExtensions
    {
        private static double AlmostZero = 1.0E-10;

        public static bool IsAlmostZero(this double value)
        {
            return Math.Abs(value) < AlmostZero;
        }
    }
}
