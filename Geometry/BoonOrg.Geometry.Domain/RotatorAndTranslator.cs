// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class RotatorAndTranslator : IRotatorAndTranslator
    {
        public double DegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public double RadiansToDegrees(double angle)
        {
            return 180.0 * angle / Math.PI;
        }

        public void TransformYZ(IPoint point, IPoint translation, double angleY, double angleZ)
        {
            angleY = DegreesToRadians(angleY);
            angleZ = DegreesToRadians(angleZ);

            double cosAngleY = Math.Cos(angleY);
            double sinAngleY = Math.Sin(angleY);

            double cosAngleZ = Math.Cos(angleZ);
            double sinAngleZ = Math.Sin(angleZ);

            RotateY(point, cosAngleY, sinAngleY);
            RotateZ(point, cosAngleZ, sinAngleZ);
            Translate(point, translation);
        }

        public void TransformYZ(ITriangleContainer model, IPoint translation, double angleY, double angleZ)
        {
            angleY = DegreesToRadians(angleY);
            angleZ = DegreesToRadians(angleZ);

            double cosAngleY = Math.Cos(angleY);
            double sinAngleY = Math.Sin(angleY);

            double cosAngleZ = Math.Cos(angleZ);
            double sinAngleZ = Math.Sin(angleZ);

            RotateY(model, cosAngleY, sinAngleY);
            RotateZ(model, cosAngleZ, sinAngleZ);
            Translate(model, translation);
        }

        public void TransformXY(ITriangleContainer model, IPoint translation, double angleX, double angleY)
        {
            angleX = DegreesToRadians(angleX);
            angleY = DegreesToRadians(angleY);

            double cosAngleX = Math.Cos(angleX);
            double sinAngleX = Math.Sin(angleX);

            double cosAngleY = Math.Cos(angleY);
            double sinAngleY = Math.Sin(angleY);

            RotateX(model, cosAngleX, sinAngleX);
            RotateY(model, cosAngleY, sinAngleY);
            Translate(model, translation);
        }

        public void TransformZX(ITriangleContainer model, IPoint translation, double angleX, double angleZ)
        {
            angleX = DegreesToRadians(angleX);
            angleZ = DegreesToRadians(angleZ);

            double cosAngleX = Math.Cos(angleX);
            double sinAngleX = Math.Sin(angleX);

            double cosAngleZ = Math.Cos(angleZ);
            double sinAngleZ = Math.Sin(angleZ);

            RotateZ(model, cosAngleZ, sinAngleZ);
            RotateX(model, cosAngleX, sinAngleX);
            Translate(model, translation);
        }

        public void RotateX(IPoint point, double angle)
        {
            angle *= Math.PI / 180.0;
            RotateX(point, Math.Cos(angle), Math.Sin(angle));
        }

        public void RotateY(IPoint point, double angle)
        {
            angle *= Math.PI / 180.0;
            RotateY(point, Math.Cos(angle), Math.Sin(angle));
        }

        public void RotateZ(IPoint point, double angle)
        {
            angle *= Math.PI / 180.0;
            RotateZ(point, Math.Cos(angle), Math.Sin(angle));
        }

        public void RotateX(IPoint point, double cosx, double sinx)
        {
            // double x = point.X;
            double y = point.Y * cosx - point.Z * sinx;
            double z = point.Y * sinx + point.Z * cosx;

            // point.X = x;
            point.Y = y;
            point.Z = z;
        }

        public void RotateY(IPoint point, double cosy, double siny)
        {
            double x = point.X * cosy + point.Z * siny;
            // double y = point.y;
            double z = -point.X * siny + point.Z * cosy;

            point.X = x;
            // point.Y = y;
            point.Z = z;
        }

        public void RotateZ(IPoint point, double cosz, double sinz)
        {
            double x = point.X * cosz - point.Y * sinz;
            double y = point.X * sinz + point.Y * cosz;
            // double z = point.Z;

            point.X = x;
            point.Y = y;
            // point.Z = z;
        }

        public void Translate(IPoint point, IPoint translation)
        {
            point.X += translation.X;
            point.Y += translation.Y;
            point.Z += translation.Z;
        }

        public void InverseTranslate(IPoint point, IPoint translation)
        {
            point.X -= translation.X;
            point.Y -= translation.Y;
            point.Z -= translation.Z;
        }

        public void RotateX(ITriangleContainer model, double cosx, double sinx)
        {
            foreach (IPoint point in model.Vertices)
            {
                // double x = point.X;
                double y = point.Y * cosx - point.Z * sinx;
                double z = point.Y * sinx + point.Z * cosx;

                // point.X = x;
                point.Y = y;
                point.Z = z;
            }
        }

        public void RotateY(ITriangleContainer model, double cosy, double siny)
        {
            foreach (IPoint point in model.Vertices)
            {
                double x = point.X * cosy + point.Z * siny;
                // double y = point.y;
                double z = -point.X * siny + point.Z * cosy;

                point.X = x;
                // point.Y = y;
                point.Z = z;
            }
        }

        public void RotateZ(ITriangleContainer model, double cosz, double sinz)
        {
            foreach (IPoint point in model.Vertices)
            {
                double x = point.X * cosz - point.Y * sinz;
                double y = point.X * sinz + point.Y * cosz;
                // double z = point.Z;

                point.X = x;
                point.Y = y;
                // point.Z = z;
            }
        }

        public void Translate(ITriangleContainer model, IPoint translation)
        {
            foreach (IPoint point in model.Vertices)
            {
                point.X += translation.X;
                point.Y += translation.Y;
                point.Z += translation.Z;
            }
        }

        public void InverseTranslate(ITriangleContainer model, IPoint translation)
        {
            foreach (IPoint point in model.Vertices)
            {
                point.X -= translation.X;
                point.Y -= translation.Y;
                point.Z -= translation.Z;
            }
        }
    }
}
