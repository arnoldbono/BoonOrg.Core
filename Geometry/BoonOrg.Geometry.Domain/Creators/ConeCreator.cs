// (c) 2017 Roland Boon

using System;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class ConeCreator : IConeCreator
    {
        private readonly Func<ICone> m_coneFunc;

        public ConeCreator(Func<ICone> coneFunc)
        {
            m_coneFunc = coneFunc;
        }

        public ICone Create() => Create(1.0, 1.0, 1.0, 20);

        public ICone Create(double radiusX, double radiusY, double height, int resolution)
        {
            var cone = m_coneFunc();
            cone.Set(radiusX, radiusY, height, resolution);
            return cone;
        }
    }
}
