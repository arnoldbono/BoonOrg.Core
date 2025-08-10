// (c) 2015-2017 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Logic;
using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <summary>
    /// A tetrahedron (a 3D mathematical shape span by four coordinates).
    /// </summary>
    internal sealed class Tetrahedron : TriangleContainer, ITetrahedron
    {
        private readonly IVectorLogic m_vectorLogic;

        public IPoint Vertex1 { get { return m_vertices[0]; } set { m_vertices[0] = value; } }

        public IPoint Vertex2 { get { return m_vertices[1]; } set { m_vertices[1] = value; } }

        public IPoint Vertex3 { get { return m_vertices[2]; } set { m_vertices[2] = value; } }

        public IPoint Vertex4 { get { return m_vertices[3]; } set { m_vertices[3] = value; } }

        public override IEnumerable<IPoint> Vertices
        {
            get
            {
                yield return Vertex1;
                yield return Vertex2;
                yield return Vertex3;
                yield return Vertex4;
            }
        }

        public Tetrahedron(IIdentity identity, IVectorLogic vectorLogic, Func<IBoundingBox> boundaryBoxFunc) : base(identity, boundaryBoxFunc)
        {
            m_vectorLogic = vectorLogic;

            SetCapacity(4, 4);
            SetTriangles();
        }

        public void Set(IReadOnlyList<IPoint> coordinates)
        {
            Vertex1 = coordinates[0].Clone();
            Vertex2 = coordinates[1].Clone();
            Vertex3 = coordinates[2].Clone();
            Vertex4 = coordinates[3].Clone();
        }

        private void SetTriangles()
        {
            SetTriangle(0, 0, 1, 2);
            SetTriangle(1, 3, 2, 1);
            SetTriangle(2, 0, 2, 3);
            SetTriangle(3, 1, 3, 0);
        }

        public override string ToString()
        {
            return $@"[{Vertex1}, {Vertex2}, {Vertex3}, {Vertex4}]";
        }

        public double GetVolume()
        {
            // http://mathworld.wolfram.com/Tetrahedron.html
            IVector vectorA = new Vector(Vertex2, Vertex1);
            IVector vectorB = new Vector(Vertex3, Vertex1);
            IVector vectorC = new Vector(Vertex4, Vertex1);
            return Math.Abs(m_vectorLogic.GetInnerProduct(vectorA, m_vectorLogic.GetCrossProduct(vectorB, vectorC))) / 6.0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Tetrahedron);
        }

        public override int GetHashCode()
        {
            return Vertex1.GetHashCode() ^ Vertex2.GetHashCode() ^ Vertex3.GetHashCode() ^ Vertex4.GetHashCode();
        }

        public bool Equals(Tetrahedron src)
        {
            if (src == null)
            {
                return false;
            }

            int count = m_vertices.Count;
            for (int i = 0; i < count; ++i)
            {
                var hit = false;
                for (int j = 0; j < count; ++j)
                {
                    if (m_vertices[i].Equals(src.m_vertices[j]))
                    {
                        hit = true;
                        break;
                    }
                }

                if (!hit)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
