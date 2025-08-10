// (c) 2017 Roland Boon
//------------------------------------------------------------------
// (c) 2009 Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------
// https://www.codeproject.com/Articles/42174/High-performance-WPF-D-Chart
//------------------------------------------------------------------
// Bar is a reworked version of Bar3D
//------------------------------------------------------------------

using System;
using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class Bar : TriangleContainer, IBar
    {
        public IPoint Center { get; private set; }
        public double Width { get; private set; }
        public double Length { get; private set; }
        public double Height { get; private set; }

        // First parameters is the bar center, the last 3 parameters are the bar size at each axis
        public Bar(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc) : base(identity, boundaryBoxFunc)
        {
        }

        // First parameters is the bar center, the last 3 parameters are the bar size at each axis
        public void Set(IPoint center, double width, double length, double height)
        {
            Center = center;
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

            SetCapacity(8, 12);
            SetTriangles();
            SetVertices();

            return this;
        }

        private void SetTriangles()
        {
            SetTriangle(0, 0, 2, 1);
            SetTriangle(1, 0, 3, 2);
            SetTriangle(2, 1, 2, 5);
            SetTriangle(3, 2, 6, 5);
            SetTriangle(4, 3, 6, 2);
            SetTriangle(5, 3, 7, 6);
            SetTriangle(6, 0, 4, 3);
            SetTriangle(7, 3, 4, 7);
            SetTriangle(8, 4, 6, 7);
            SetTriangle(9, 4, 5, 6);
            SetTriangle(10, 0, 5, 4);
            SetTriangle(11, 0, 1, 5);
        }

        private void SetVertices()
        {
            double halfWidth = Width / 2;
            double halfLength = Length / 2;
            double halfHeight = Height / 2;

            double minX = Center.X - halfWidth;
            double maxX = Center.X + halfWidth;
            double minY = Center.Y - halfLength;
            double maxY = Center.Y + halfLength;
            double minZ = Center.Z - halfHeight;
            double maxZ = Center.Z + halfHeight;

            SetVertex(0, minX, maxY, maxZ);
            SetVertex(1, maxX, maxY, maxZ);
            SetVertex(2, maxX, minY, maxZ);
            SetVertex(3, minX, minY, maxZ);

            SetVertex(4, minX, maxY, minZ);
            SetVertex(5, maxX, maxY, minZ);
            SetVertex(6, maxX, minY, minZ);
            SetVertex(7, minX, minY, minZ);
        }
    }
}
