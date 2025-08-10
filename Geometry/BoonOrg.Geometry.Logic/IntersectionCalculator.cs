// (c) 2017 Roland Boon

using System.Collections.Generic;
using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class IntersectionCalculator : IIntersectionCalculator
    {
        private readonly IPointCreator m_pointCreator;
        private readonly IVectorLogic m_vectorLogic;

        public IntersectionCalculator(IPointCreator pointCreator,
            IVectorLogic vectorLogic)
        {
            m_pointCreator = pointCreator;
            m_vectorLogic = vectorLogic;
        }

        /// <inheritdoc/>
        public IPoint ComputeIntersectionAtDepth(IPoint above, IPoint below, double depth)
        {
            double z = -depth;
            double f = (z - below.Z) / (above.Z - below.Z);
            System.Diagnostics.Debug.Assert(f >= 0.0 && f <= 1.0);

            double x = below.X + f * (above.X - below.X);
            double y = below.Y + f * (above.Y - below.Y);
            return m_pointCreator.Create(x, y, z);
        }

        public IPoint ComputeIntersectionWithPlane(IPoint above, IPoint below, IPlane plane)
        {
            IPoint center = plane.Center;
            IPoint tc = m_pointCreator.Create(center.X - below.X, center.Y - below.Y, center.Z - below.Z);
            double t = plane.GetInnerProductWithNormal(tc);
            IPoint nc = m_pointCreator.Create(above.X - below.X, above.Y - below.Y, above.Z - below.Z);
            double n = plane.GetInnerProductWithNormal(nc);
            double f = t / n;
            System.Diagnostics.Debug.Assert(f >= 0.0 && f <= 1.0);
            return m_pointCreator.Create(below.X + f * nc.X, below.Y + f * nc.Y, below.Z + f * nc.Z);
        }

        public int ComputeCoordinatesAboveDepth(IEnumerable<IPoint> coordinates, double depth)
        {
            double depthZ = -depth;

            int above = 0;
            foreach (IPoint c in coordinates)
            {
                if (c.Z > depthZ)
                {
                    above++;
                }
            }
            return above;
        }

        public int ComputeCoordinatesBelowDepth(IEnumerable<IPoint> coordinates, double depth)
        {
            double depthZ = -depth;

            int below = 0;
            foreach (IPoint c in coordinates)
            {
                if (c.Z < depthZ)
                {
                    below++;
                }
            }
            return below;
        }

        public int ComputeCoordinatesAbovePlane(IEnumerable<IPoint> coordinates, IPlane plane)
        {
            int above = 0;
            foreach (IPoint c in coordinates)
            {
                if (plane.GetRelation(c) == RelationToPlane.Above)
                {
                    above++;
                }
            }
            return above;
        }

        public int ComputeCoordinatesBelowPlane(IEnumerable<IPoint> coordinates, IPlane plane)
        {
            int below = 0;
            foreach (IPoint c in coordinates)
            {
                if (plane.GetRelation(c) == RelationToPlane.Below)
                {
                    below++;
                }
            }
            return below;
        }

        public IPoint MapToDepth(IPoint coordinate, double depth)
        {
            return m_pointCreator.Create(coordinate.X, coordinate.Y, -depth);
        }

        public IPoint MapToPlane(IPoint coordinate, IPlane plane)
        {
            double d = plane.GetTranslatedInnerProductWithNormal(coordinate);
            return plane.Normal.Translate(coordinate, -d);
        }
    }
}
