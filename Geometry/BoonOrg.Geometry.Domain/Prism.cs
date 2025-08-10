// (c) 2015-2017 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <inheritdoc/>
    internal sealed class Prism : TriangleContainer, IPrism
    {
        public ITriangle SideA { get { return GetTriangle(0); } }

        public ITriangle SideB { get { return GetTriangle(1); } }

        public Prism(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc) : base(identity, boundaryBoxFunc)
        {
            SetCapacity(6, 8);
            SetTriangles();
        }

        public void Set(IReadOnlyList<IPoint> coordinates)
        {
            int count = coordinates.Count;
            for (int i = 0; i < count; ++i)
            {
                m_vertices[i] = coordinates[i];
            }
        }

        public ITriangleContainer Create()
        {
            return this;
        }

        private void SetTriangles()
        {
            SetTriangle(0, 0, 2, 1);
            SetTriangle(1, 5, 3, 4);
            SetTriangle(2, 0, 5, 2);
            SetTriangle(3, 0, 3, 5);
            SetTriangle(4, 1, 2, 5);
            SetTriangle(5, 1, 5, 4);
            SetTriangle(6, 0, 1, 4);
            SetTriangle(7, 0, 4, 3);
        }

        public double GetVolume()
        {
            // http://mathworld.wolfram.com/Prism.html
            return 0.0;
        }
    }
}
