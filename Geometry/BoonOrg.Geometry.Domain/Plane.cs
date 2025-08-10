// (c) 2015-2017 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Identification;

using BoonOrg.Geometry.Logic;

namespace BoonOrg.Geometry.Domain
{
    /// <inheritdoc/>
    internal sealed class Plane : Surface, IPlane
    {
        private readonly IVectorLogic m_vectorLogic;
        private readonly Func<IBoundingBox> m_boundingBoxFunc;

        public IPoint Center { get; private set; }

        public IVector Normal { get; private set; }

        public double Distance => GetDistance(new Point());

        public double Width { get; set; } = 1.0;

        public double Length { get; set; } = 1.0;

        public IEnumerable<IPoint> Vertices
        {
            get
            {
                IVector tangent1 = m_vectorLogic.GetCrossProduct(Normal, new Vector(0.0, 1.0, 0.0));
                double length = tangent1.Length;
                if (length == 0.0)
                {
                    tangent1 = m_vectorLogic.GetCrossProduct(Normal, new Vector(0.0, 0.0, 1.0));
                    length = tangent1.Length;
                    if (length == 0.0)
                    {
                        tangent1 = m_vectorLogic.GetCrossProduct(Normal, new Vector(1.0, 0.0, 0.0));
                        length = tangent1.Length;
                    }
                }
                IVector tangent2 = m_vectorLogic.GetCrossProduct(Normal, tangent1);
                tangent1.Scale(1.0 / length);
                tangent2.Normalize();

                IVector center = new Vector(Center);

                // Get a polyline that forms a rectangle when closed.
                yield return m_vectorLogic.Span(center, -Width / 2.0, Length / 2.0, tangent1, tangent2);
                yield return m_vectorLogic.Span(center, Width / 2.0, Length / 2.0, tangent1, tangent2);
                yield return m_vectorLogic.Span(center, Width / 2.0, -Length / 2.0, tangent1, tangent2);
                yield return m_vectorLogic.Span(center, -Width / 2.0, -Length / 2.0, tangent1, tangent2);
            }
        }

        public Plane(IIdentity identity, IVectorLogic vectorLogic, Func<IBoundingBox> boundingBoxFunc) : base(identity)
        {
            Center = new Point();
            Normal = new Vector();
            m_vectorLogic = vectorLogic;
            m_boundingBoxFunc = boundingBoxFunc;
        }

        public void Set(IPoint center, IVector normal, string name)
        {
            Center.Set(center);
            double length = normal.Length;
            Normal.Set(normal.X / length, normal.Y / length, normal.Z / length);
            Identification.Rename(name);
            Identification.ResetId(Guid.NewGuid());
        }

        public double GetDistance(IPoint coordinate)
        {
            // Nykamp DQ, “Distance from point to plane.” From Math Insight.http://mathinsight.org/distance_point_plane
            return Math.Abs(GetTranslatedInnerProductWithNormal(coordinate));
        }

        public double GetTranslatedInnerProductWithNormal(IPoint coordinate)
        {
            return m_vectorLogic.GetInnerProduct(Normal, new Vector(Center, coordinate));
        }

        public double GetInnerProductWithNormal(IPoint coordinate)
        {
            return m_vectorLogic.GetInnerProduct(Normal, new Vector(coordinate));
        }

        public RelationToPlane GetRelation(IPoint coordinate)
        {
            double innerProduct = GetTranslatedInnerProductWithNormal(coordinate);
            if (innerProduct > 0.0)
            {
                return RelationToPlane.Above;
            }
            if (innerProduct < 0.0)
            {
                return RelationToPlane.Below;
            }
            return RelationToPlane.Intersecting;
        }

        public override IBoundingBox GetBoundingBox()
        {
            var boudingBox = m_boundingBoxFunc();
            boudingBox.Expand(Vertices);
            return boudingBox;
        }

        public override void ExpandBoundingBox(IBoundingBox box)
        {
            box.Expand(Vertices);
        }
    }
}
