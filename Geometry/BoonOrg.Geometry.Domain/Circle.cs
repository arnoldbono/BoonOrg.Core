// (c) 2015-2017 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <inheritdoc/>
    internal sealed class Circle : ICircle
    {
        private readonly IIdentity m_identity;
        private readonly Func<IBoundingBox> m_boundingBoxFunc;

        public IIdentity Identification => m_identity;

        public IPoint Center { get; private set; }

        public IVector Normal { get; private set; }

        public double Radius { get; set; } = 1.0;

        public int Count { get; set; } = 100;

        public Circle(IIdentity identity, Func<IBoundingBox> boundingBoxFunc)
        {
            m_identity = identity;
            m_boundingBoxFunc = boundingBoxFunc;
            m_identity.SetOwner(this);

            Center = new Point();
            Normal = new Vector();
        }

        public void Set(IPoint center, IVector normal, string name)
        {
            if (center == null)
            {
                center = new Point();
            }
            Center.Set(center);
            if (normal == null)
            {
                normal = new Vector(0.0, 0.0, 1.0);
            }
            if (string.IsNullOrEmpty(name))
            {
                name = @"Circle";
            }

            double length = normal.Length;
            Normal.Set(normal.X / length, normal.Y / length, normal.Z / length);
            Identification.Rename(name);
            Identification.ResetId(Guid.NewGuid());
        }

        public IEnumerable<IPoint> Vertices
        {
            get
            {
                var v3x = Normal.X;
                var v3y = Normal.Y;
                var v3z = Normal.Z;

                // Calculate v1.
                var s = v3x * v3x + v3z * v3z;
                double v1x, v1y, v1z;
                if (s < 1.0E-200)
                {
                    v1x = 1.0;
                    v1y = 0.0;
                    v1z = 0.0;
                }
                else
                {
                    v1x = v3z / s;
                    v1y = 0.0f;
                    v1z = -v3x / s;
                }

                // Calculate v2 as cross product of v3 and v1.
                var v2x = v3y * v1z - v3z * v1y;
                var v2y = v3z * v1x - v3x * v1z;
                var v2z = v3x * v1y - v3y * v1x;

                var cx = Center.X;
                var cy = Center.Y;
                var cz = Center.Z;

                var r = Radius;

                for (int i = 0; i < Count; i++)
                {
                    var a = 2.0 * Math.PI * i / (double)Count;

                    var cosa = Math.Cos(a);
                    var sina = Math.Sin(a);

                    var px = cx + r * (v1x * cosa + v2x * sina);
                    var py = cy + r * (v1y * cosa + v2y * sina);
                    var pz = cz + r * (v1z * cosa + v2z * sina);

                    yield return new Point(px, py, pz);
                }
            }
        }

        public IBoundingBox GetBoundingBox()
        {
            var boudingBox = m_boundingBoxFunc();
            boudingBox.Expand(Vertices);
            return boudingBox;
        }

        public void ExpandBoundingBox(IBoundingBox box)
        {
            box.Expand(Vertices);
        }
    }
}
