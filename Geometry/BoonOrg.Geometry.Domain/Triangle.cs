// (c) 2017 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Domain
{
    /// <summary>
    /// A triangle (a 3D mathematical shape span by three coordinates).
    /// </summary>
    internal sealed class Triangle : ITriangle
    {
        private readonly Func<IBoundingBox> m_boundingBoxFunc;

        public IPoint Vertex1 { get; set; }
        public IPoint Vertex2 { get; set; }
        public IPoint Vertex3 { get; set; }

        public IEnumerable<IPoint> Vertices
        {
            get
            {
                yield return Vertex1;
                yield return Vertex2;
                yield return Vertex3;
            }
        }

        public Triangle(Func<IBoundingBox> boundingBoxFunc)
        {
            m_boundingBoxFunc = boundingBoxFunc;
            Vertex1 = new Point();
            Vertex2 = new Point();
            Vertex3 = new Point();
        }

        public void Assign(IPoint c1, IPoint c2, IPoint c3)
        {
            Vertex1 = c1.Clone();
            Vertex2 = c2.Clone();
            Vertex3 = c3.Clone();
        }

        public void Assign(IReadOnlyList<IPoint> coordinates)
        {
            Vertex1 = coordinates[0].Clone();
            Vertex2 = coordinates[1].Clone();
            Vertex3 = coordinates[2].Clone();
        }

        public override string ToString()
        {
            return $@"[{Vertex1}, {Vertex2}, {Vertex3}]";
        }

        //public static double ComputeArea(IPoint vertex1, IPoint vertex2, IPoint vertex3)
        //{
        //    // http://mathworld.wolfram.com/TriangleArea.html
        //    double a2 = Point.ComputeLength2(vertex1, vertex2);
        //    double b2 = Point.ComputeLength2(vertex2, vertex3);
        //    double c2 = Point.ComputeLength(vertex3, vertex1);
        //    return 0.25 * Math.Sqrt(2.0 * b2 * c2 + 2.0 * c2 * a2 + 2.0 * a2 * b2 - a2 * a2 - b2 * b2 - c2 * c2);
        //}

        //public double ComputeArea()
        //{
        //    return ComputeArea(Vertex1, Vertex2, Vertex3);
        //}

        public IVector ComputeNormal()
        {
            double dx1 = Vertex2.X - Vertex1.X;
            double dy1 = Vertex2.Y - Vertex1.Y;
            double dz1 = Vertex2.Z - Vertex1.Z;

            double dx2 = Vertex3.X - Vertex1.X;
            double dy2 = Vertex3.Y - Vertex1.Y;
            double dz2 = Vertex3.Z - Vertex1.Z;

            double vx = dy1 * dz2 - dz1 * dy2;
            double vy = dz1 * dx2 - dx1 * dz2;
            double vz = dx1 * dy2 - dy1 * dx2;

            double length = Math.Sqrt(vx * vx + vy * vy + vz * vz);

            return new Vector(vx / length, vy / length, vz / length);
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

        public void Translate(IVector vector)
        {
            for (int i = 0; i < 3; ++i)
            {
                Vertex1[i] += vector[i];
                Vertex2[i] += vector[i];
                Vertex3[i] += vector[i];
            }
        }

        public void Flip()
        {
            var tmp = Vertex3;
            Vertex3 = Vertex1;
            Vertex1 = tmp;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Triangle);
        }

        public override int GetHashCode()
        {
            return Vertex1.GetHashCode() ^ Vertex2.GetHashCode() ^ Vertex3.GetHashCode();
        }

        public bool Equals(Triangle src)
        {
            if (src == null)
            {
                return false;
            }

            foreach (var mv in Vertices)
            {
                bool match = false;
                foreach (var v in src.Vertices)
                {
                    if (mv.Equals(v))
                    {
                        match = true;
                        break;
                    }
                }
                if (!match)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
