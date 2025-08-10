// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Geometry.Domain
{
    public class Point : IPoint
    {
        protected readonly double[] m_coordinates = new double[3] { 0.0, 0.0, 0.0 };

        public double X { get { return m_coordinates[0]; } set { m_coordinates[0] = value; } }
        public double Y { get { return m_coordinates[1]; } set { m_coordinates[1] = value; } }
        public double Z { get { return m_coordinates[2]; } set { m_coordinates[2] = value; } }

        public Point()
        {
        }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point(double[] point)
        {
            if (point.Length == 3)
            {
                X = point[0];
                Y = point[1];
                Z = point[2];
            }
        }

        public double this[int i]
        {
            get { return GetAt(i); }
            set { SetAt(i, value); }
        }

        public double Length2 => X * X + Y * Y + Z * Z;

        public double Length
        {
            get { return Math.Sqrt(Length2); }
            set { Scale(value / Length); }
        }

        public void Set(IPoint point)
        {
            for (int i = 0; i < 3; i++)
            {
                m_coordinates[i] = point[i];
            }
        }

        public void SetAt(int i, double d)
        {
            m_coordinates[i] = d;
        }

        public void Set(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double GetAt(int i)
        {
            return m_coordinates[i];
        }

        public void Scale(double scale)
        {
            for (int i = 0; i < 3; ++i)
            {
                m_coordinates[i] *= scale;
            }
        }

        public void Zero()
        {
            Assign(0.0);
        }

        public void Assign(double value)
        {
            for (int i = 0; i < 3; ++i)
            {
                m_coordinates[i] = value;
            }
        }

        public bool IsSame(IPoint point)
        {
            if (point == null)
            {
                return false;
            }
            return point.X == X && point.Y == Y && point.Z == Z;
        }

        public bool IsSame(IPoint point, double accuracy)
        {
            if (point == null)
            {
                return false;
            }
            return Math.Abs(point.X - X) < accuracy &&
                Math.Abs(point.Y - Y) < accuracy &&
                Math.Abs(point.Z - Z) < accuracy;
        }

        public override string ToString()
        {
            return string.Format("[X: {0}, Y: {1}, Z: {2}]", X, Y, Z);
        }

        public IPoint Clone()
        {
            return new Point(X, Y, Z);
        }
    }
}
