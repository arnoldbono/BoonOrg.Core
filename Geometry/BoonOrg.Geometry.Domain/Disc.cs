// (c) 2020 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <inheritdoc/>
    internal sealed class Disc : TriangleContainer, IDisc
    {
        private readonly ICircle m_circle;

        public IPoint Center => m_circle.Center;

        public IVector Normal => m_circle.Normal;

        public double Radius { get => m_circle.Radius; set => m_circle.Radius = value; }

        public int Count { get => m_circle.Count; set => m_circle.Count = value; }

        public Disc(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc) : base(identity, boundaryBoxFunc)
        {
            m_circle = new Circle(identity, boundaryBoxFunc);
        }

        public void Set(IPoint center, IVector normal, string name)
        {
            m_circle.Set(center, normal, name);
        }

        public override IEnumerable<IPoint> Vertices
        {
            get => m_circle.Vertices;
        }

        public override IEnumerable<IVector> Normals
        {
            get
            {
                var count = Count;
                for (int i = 0; i < count; i++)
                {
                    yield return Normal;
                }
            }
        }
    }
}
