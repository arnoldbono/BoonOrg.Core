// (c) 2017 Roland Boon
//------------------------------------------------------------------
// (c) 2009 Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------
// https://www.codeproject.com/Articles/42174/High-performance-WPF-D-Chart
//------------------------------------------------------------------
// Pyramid is a reworked version of Pyramid3D (which was in fact a tetrahedron).
//------------------------------------------------------------------

using System;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class Pyramid : TriangleContainer, IPyramid
    {
        public double Width { get; private set; }

        public double Length { get; private set; }

        public double Height { get; private set; }

        public Pyramid(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc) : base(identity, boundaryBoxFunc)
        {
            Set(1.0);
        }

        public void Set(double size)
        {
            Set(size, size * Math.Sqrt(3.0) / 2, size * Math.Sqrt(2.0 / 3.0));
        }

        public void Set(double width, double length, double height)
        {
            Width = width;
            Length = length;
            Height = height;
        }

        public ITriangleContainer Create()
        {
            if (m_vertices.Count != 0)
            {
                return this;
            }

            SetCapacity(5, 6);
            SetTriangles();
            SetVertices();

            return this;
        }

        private void SetTriangles()
        {
            SetTriangle(0, 0, 2, 1);
            SetTriangle(1, 0, 3, 2);
            SetTriangle(2, 0, 1, 4);
            SetTriangle(3, 1, 2, 4);
            SetTriangle(4, 2, 3, 4);
            SetTriangle(5, 3, 0, 4);
        }

        private void SetVertices()
	    {
            SetVertex(0, -Width / 2, -Length / 2, 0);
            SetVertex(1, Width / 2, -Length / 2, 0);
            SetVertex(2, Width / 2, Length / 2, 0);
            SetVertex(3, -Width / 2, Length / 2, 0);
            SetVertex(4, 0, 0, Height);
        }
    }
}
