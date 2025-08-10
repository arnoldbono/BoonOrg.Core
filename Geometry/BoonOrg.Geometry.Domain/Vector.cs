// (c) 2015, 2017 Roland Boon

namespace BoonOrg.Geometry.Domain
{
    /// <summary>
    /// A 3D vector pointing from (0, 0, 0) to (X, Y, Z).
    /// </summary>
    public class Vector : Point, IVector
    {
        public Vector()
        {
        }

        public Vector(IPoint v) : base(v.X, v.Y, v.Z)
        {
        }

        public Vector(IPoint p1, IPoint p2) : base(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z)
        {
        }

        public Vector(double x, double y, double z) : base(x, y, z)
        {
        }

        public Vector(double[] vector)
        {
            if (vector.Length == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    m_coordinates[i] = vector[i];
                }
            }
        }

        public void Set(IVector vector)
        {
            for (int i = 0; i < 3; i++)
            {
                m_coordinates[i] = vector[i];
            }
        }

        public IPoint ToPoint()
        {
            var point = new Point();
            point.Set(this);
            return point;
        }

        public IVector Translate(IVector translation, double factor)
        {
            return new Vector(translation.Translate(ToPoint(), factor));
        }

        public IPoint Translate(IPoint coordinate, double factor)
        {
            return new Point(coordinate.X + factor * X,
                coordinate.Y + factor * Y,
                coordinate.Z + factor * Z);
        }

        public void Normalize()
        {
            double length = Length;
            if (length != 0.0)
            {
                Scale(1.0 / length);
            }
        }

        public override string ToString()
        {
            return string.Format("[X: {0}, Y: {1}, Z: {2}]", X, Y, Z);
        }


        public override bool Equals(object obj)
        {
            return Equals(obj as IVector);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        public bool Equals(IVector src)
        {
            return src != null && X == src.X && Y == src.Y && Z == src.Z;
        }

        public new IVector Clone()
        {
            var vector = new Vector();
            vector.Set(this);
            return vector;
        }
    }
}
