// (c) 2017, 2023 Roland Boon

using System;

namespace BoonOrg.Geometry.Logic
{
    /// <inheritdoc/>
    internal sealed class VectorLogic : IVectorLogic
    {
        private const double RadianToDegreeFactor = 180.0 / Math.PI;

        private readonly Func<IVector> m_vectorFunc;
        private readonly Func<IPoint> m_pointFunc;

        public VectorLogic(Func<IVector> vectorFunc, Func<IPoint> pointFunc)
        {
            m_vectorFunc = vectorFunc;
            m_pointFunc = pointFunc;
        }

        public IPoint Span(IPoint point, double scale1, double scale2, IVector vector1, IVector vector2)
        {
            var retval = m_pointFunc();
            retval.Set(
                point.X + scale1 * vector1.X + scale2 * vector2.X,
                point.Y + scale1 * vector1.Y + scale2 * vector2.Y,
                point.Z + scale1 * vector1.Z + scale2 * vector2.Z);
            return retval;
        }

        public IPoint Span(double scale1, double scale2, IPoint point1, IPoint point2)
        {
            var retval = m_pointFunc();
            retval.Set(
                scale1 * point1.X + scale2 * point2.X,
                scale1 * point1.Y + scale2 * point2.Y,
                scale1 * point1.Z + scale2 * point2.Z);
            return retval;
        }

        public void Translate(IPoint point, IVector translation, double factor)
        {
            for (int i = 0; i < 3; ++i)
            {
                point[i] += factor * translation[i];
            }
        }

        public void Translate(IVector vector, IVector translation, double factor)
        {
            for (int i = 0; i < 3; ++i)
            {
                vector[i] += factor * translation[i];
            }
        }

        public IVector GetCrossProduct(IVector v1, IVector v2)
        {
            var retval = m_vectorFunc();
            retval.Set(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X);
            return retval;
        }

        public double GetInnerProduct(IVector v1, IVector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public IVector Weighed(IVector vectorU, IVector vectorV, double factorU, double factorV)
        {
            var retval = m_vectorFunc();
            for (int i = 0; i < 3; ++i)
            {
                retval[i] = factorU * vectorU[i] + factorV + vectorV[i];
            }
            return retval;
        }

        public double ComputeLength(IVector vector1, IVector vector2)
        {
            return Math.Sqrt(ComputeLength2(vector1, vector2));
        }

        public double ComputeLength2(IVector vector1, IVector vector2)
        {
            double d = vector1.X - vector2.X;
            double retval = d * d;
            d = vector1.Y - vector2.Y;
            retval += d * d;
            d = vector1.Z - vector2.Z;
            retval += d * d;
            return retval;
        }

        public double ComputeLength(IPoint point1, IPoint point2)
        {
            return Math.Sqrt(ComputeLength2(point1, point2));
        }

        public double ComputeLength2(IPoint point1, IPoint point2)
        {
            double d = point1.X - point2.X;
            double retval = d * d;
            d = point1.Y - point2.Y;
            retval += d * d;
            d = point1.Z - point2.Z;
            retval += d * d;
            return retval;
        }

        public void GenerateOrthogonalBasis(IVector normal, out IVector U, out IVector V)
        {
            U = m_vectorFunc();
            if (normal[2] == 0.0)
            {
                U.Set(normal[1], -normal[0], 0.0);
            }
            else
            {
                U.Set(normal[2], 0.0, -normal[0]);
            }
            V = GetCrossProduct(normal, U);
        }

        public bool AimZ(IVector tangent, out double rotateX, out double rotateY)
        {
            rotateX = 0.0;
            rotateY = 0.0;

            double lengthXZ2 = tangent.X * tangent.X + tangent.Z * tangent.Z;
            double lengthXYZ2 = lengthXZ2 + tangent.Y * tangent.Y;

            if (lengthXYZ2 == 0.0)
            {
                return false;
            }

            double lengthXZ = Math.Sqrt(lengthXZ2);
            double lengthXYZ = Math.Sqrt(lengthXYZ2);

            // The initial rotation, about the y axis, is given by the projection of
            // the direction vector onto the x,z plane: the x and z components
            // of the direction.
            rotateY = (lengthXZ == 0.0) ?
                ((tangent.Y > 0.0) ? -90.0 : +90.0) :
                (Math.Acos(tangent.Z / lengthXZ) * RadianToDegreeFactor);

            // The second rotation, about the x axis, is given by the projection on
            // the y,z plane of the y-rotated direction vector: the original y
            // component, and the rotated x,z vector from above.
            rotateX = Math.Acos(lengthXZ / lengthXYZ) * RadianToDegreeFactor;

            if (tangent.Y <= 0.0)
            {
                rotateX *= -1.0;
            }

            if (tangent.X > 0.0)
            {
                rotateY *= -1.0;
            }
            return true;
        }

        public void Subtract(IVector vectorOut, IVector vector1, IVector vector2)
        {
            for (int i = 0; i < 3; ++i)
            {
                vectorOut[i] = vector1[i] - vector2[i];
            }
        }

        public IVector Subtract(IVector vector1, IVector vector2)
        {
            var vector = m_vectorFunc();
            Subtract(vector, vector1, vector2);
            return vector;
        }

        public IVector Subtract(IPoint pointA, IPoint pointB)
        {
            var retval = m_vectorFunc();
            retval.Set(
                pointA.X - pointB.X,
                pointA.Y - pointB.Y,
                pointA.Z - pointB.Z);
            return retval;
        }

        public IPoint Addition(IPoint pointA, IPoint pointB)
        {
            var retval = m_vectorFunc();
            retval.Set(
                pointA.X + pointB.X,
                pointA.Y + pointB.Y,
                pointA.Z + pointB.Z);
            return retval;
        }

        public IVector FindUpVector(IVector normal)
        {
            // 0 = n1 * u1 + n2 * u2 + n3 * u3

            var vector = m_vectorFunc();

            if (normal[0] != 0.0)
            {
                vector.Set(-(normal[1] + normal[2]) / normal[0], 1.0, 1.0);
            }
            else if (normal[1] != 0.0)
            {
                vector.Set(1.0, -(normal[0] + normal[2]) / normal[1], 1.0);
            }
            else if (normal[2] != 0.0)
            {
                vector.Set(1.0, 1.0, -(normal[0] + normal[1]) / normal[2]);
            }
            else
            {
                vector.Set(0.0, 0.0, 1.0);
            }

            return vector;
        }

        public void ComputeSpanVectors(IVector normal, IVector up, out IVector span1, out IVector span2)
        {
            span1 = m_vectorFunc();
            span2 = m_vectorFunc();

            var lengthNormal = normal.Length2;
            if (lengthNormal == 0.0)
            {
                for (var i = 0; i < 3; ++i)
                {
                    span1[i] = (i == 0) ? 1.0 : 0.0;
                    span2[i] = (i == 1) ? 1.0 : 0.0;
                }
            }
            else
            {
                if (up.Length2 == 0.0)
                {
                    var j = -1;
                    lengthNormal = Math.Sqrt(lengthNormal);
                    for (var i = 0; i < 3; ++i)
                    {
                        var abs = Math.Abs(normal[i]);
                        if (lengthNormal > abs)
                        {
                            lengthNormal = abs;
                            j = i;
                        }
                    }

                    up = m_vectorFunc();
                    for (var i = 0; i < 3; ++i)
                    {
                        up[i] = (i == j) ? 1.0 : 0.0;
                    }
                }

                span1.Set(GetCrossProduct(normal, up));
                span2.Set(GetCrossProduct(normal, span1));

                span1.Normalize();
                span2.Normalize();
            }
        }

        public IVector ProjectNormalToPlane(IVector planeNormal, IVector normal)
        {
            var scaledNormal = planeNormal.Clone();
            scaledNormal.Scale(GetInnerProduct(normal, scaledNormal));
            var projectedNormal = Subtract(normal, scaledNormal);
            projectedNormal.Normalize();
            return projectedNormal;
        }

        public bool AlmostEquals(IPoint a, IPoint b)
        {
            return ComputeLength2(a, b) < 0.0001;
        }
    }
}
