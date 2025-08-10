// (c) 2017, 2023 Roland Boon

using System;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class PlaneCreator : IPlaneCreator
    {
        private readonly Func<IPlane> m_planeFunc;

        public PlaneCreator(Func<IPlane> planeFunc)
        {
            m_planeFunc = planeFunc;
        }

        public IPlane Create()
        {
            return Create(new Point(), new Vector(0.0, 0.0, 1.0), @"Plane");
        }

        public IPlane Create(IPoint center, IVector normal, string name)
        {
            var plane = m_planeFunc();
            plane.Set(center, normal, name);
            return plane;
        }
    }
}
