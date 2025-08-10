// (c) 2017 Roland Boon
//------------------------------------------------------------------
// (c) 2009 Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------
// https://www.codeproject.com/Articles/42174/High-performance-WPF-D-Chart
//------------------------------------------------------------------
// Ellipsoid is a reworked version of Ellipse3D.
//------------------------------------------------------------------

using System;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class Ellipsoid : TriangleContainer, IEllipsoid
    {
        public double RadiusX { get; private set; }
        public double RadiusY { get; private set; }
        public double Height { get; private set; }
        public int Resolution { get; private set; }

        // the first 3 parameters are the ellipse size, last parameter is the smoothness of the ellipse
        public Ellipsoid(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc) : base(identity, boundaryBoxFunc)
        {
        }

        public void Set(double radiusX, double radiusY, double height, int resolution)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
            Height = height;
            Resolution = resolution;
        }

        public ITriangleContainer Create()
        {
            if (m_vertices.Count != 0)
            {
                return this;
            }

            // one vertex at top and bottom, other ring has Resolution vertex
            int vertices = (Resolution - 2) * Resolution + 2;
            // the top and bottom band has Resolution triangle
            // middle band has 2 * Resolution * (Resolution - 2 - 1.
            int triangles = 2 * Resolution * (Resolution - 3) + 2 * Resolution;

            SetCapacity(vertices, triangles);
            SetTriangles(vertices);
            SetVertices();

            return this;
        }

        private void SetTriangles(int vertices)
        {
            // set triangle
            int n00, n01, n10, n11;
            int nTriIndex = 0;
            int nI2;
            // first set top band
            int i;
            int j = 1;
            for (i = 0; i < Resolution; i++)
            {
                if (i == (Resolution - 1)) nI2 = 0;
                else nI2 = i + 1;

                n00 = 1 + (j - 1) * Resolution + i;
                n10 = 1 + (j - 1) * Resolution + nI2;
                n01 = 0;

                SetTriangle(nTriIndex, n00, n10, n01);
                nTriIndex++;
            }
            // set middle bands
            for (j = 1; j < (Resolution - 2); j++)
            {
                for (i = 0; i < Resolution; i++)
                {
                    if (i == (Resolution - 1)) nI2 = 0;
                    else nI2 = i + 1;
                    n00 = 1 + (j - 1) * Resolution + i;
                    n10 = 1 + (j - 1) * Resolution + nI2;
                    n01 = 1 + j * Resolution + i;
                    n11 = 1 + j * Resolution + nI2;

                    SetTriangle(nTriIndex, n00, n01, n10);
                    SetTriangle(nTriIndex + 1, n01, n11, n10);
                    nTriIndex += 2;
                }
            }

            j = Resolution - 2;
            for (i = 0; i < Resolution; i++)
            {
                if (i == (Resolution - 1)) nI2 = 0;
                else nI2 = i + 1;

                n00 = 1 + (j - 1) * Resolution + i;
                n10 = 1 + (j - 1) * Resolution + nI2;
                n01 = vertices - 1;

                SetTriangle(nTriIndex, n00, n01, n10);
                nTriIndex++;
            }
        }

        private void SetVertices()
        {
            double aXYStep = 2.0 * Math.PI / Resolution;
            double aZStep = Math.PI / (Resolution - 1);

            SetVertex(0, 0, 0, Height); // first vertex is at top

            int i, j;
            double x1, y1, z1;

            double[] cosAngleXYs = new double[Resolution];
            double[] sinAngleXYs = new double[Resolution];

            for (i = 0; i < Resolution; i++)
            {
                double aXY = i * aXYStep;
                cosAngleXYs[i] = Math.Cos(aXY);
                sinAngleXYs[i] = Math.Sin(aXY);
            }

            for (j = 1; j < (Resolution - 1); j++)
            {
                double aZAngle = j * aZStep;

                double cosAngleZ = Math.Cos(aZAngle);
                double sinAngleZ = Math.Sin(aZAngle);

                for (i = 0; i < Resolution; i++)
                {
                    x1 = RadiusX * sinAngleZ * cosAngleXYs[i];
                    y1 = RadiusY * sinAngleZ * sinAngleXYs[i];
                    z1 = Height * cosAngleZ;
                    SetVertex((j - 1) * Resolution + i + 1, x1, y1, z1);
                }
            }
            SetVertex((Resolution - 2) * Resolution + 1, 0, 0, -Height);
        }
    }
}
