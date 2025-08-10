// (c) 2017, 2023 Roland Boon

using System;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class EllipsoidCreator : IEllipsoidCreator
    {
        private readonly Func<IEllipsoid> m_ellipsoidFunc;

        public EllipsoidCreator(Func<IEllipsoid> ellipsoidFunc)
        {
            m_ellipsoidFunc = ellipsoidFunc;
        }

        public IEllipsoid Create() => Create(1.0, 3.0, 1.0, 20);

        public IEllipsoid Create(double radiusX, double radiusY, double height, int resolution)
        {
            var ellipsoid = m_ellipsoidFunc();
            ellipsoid.Set(radiusX, radiusY, height, resolution);
            return ellipsoid;
        }
    }
}
