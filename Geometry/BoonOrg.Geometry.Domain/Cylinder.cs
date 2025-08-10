// (c) 2017, 2023 Roland Boon
//------------------------------------------------------------------
// (c) 2009 Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------
// https://www.codeproject.com/Articles/42174/High-performance-WPF-D-Chart
//------------------------------------------------------------------
// Cylinder is a reworked version of Cylinder3D.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using BoonOrg.Geometry.Creators;
using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <summary>
    /// A cylinder with a given resolution (level of detail).
    /// </summary>
    internal sealed class Cylinder : TriangleContainer, ICylinder
    {
        private readonly IPointCreator m_pointCreator;

        public double RadiusX { get; private set; }
        public double RadiusY { get; private set; }
        public double Height { get; private set; }
        public int Resolution { get; private set; }

        public Cylinder(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc,
            IPointCreator pointCreator) :
            base(identity, boundaryBoxFunc)
        {
            m_pointCreator = pointCreator;
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

            int vertexCount = 4 * Resolution + 2;
            int triangleCount = 4 * Resolution;
            SetCapacity(vertexCount, triangleCount);

            SetTriangles(Resolution);
            SetVertices(RadiusX, RadiusY, Height, Resolution);

            return this;
        }

        private void SetTriangles(int resolution)
        {
            var index = 0;
            for (int i = 0; i < resolution; ++i)
            {
                int n1 = i;
                int n2 = (i == resolution - 1) ? 0 : i + 1;

                SetTriangle(index++, n1, n2, resolution + n1); // side
                SetTriangle(index++, resolution + n1, n2, resolution + n2); // side
                SetTriangle(index++, 2 * resolution + n2, 2 * resolution + n1, 4 * resolution); // bottom
                SetTriangle(index++, 3 * resolution + n1, 3 * resolution + n2, 4 * resolution + 1); // top
            }
        }

        private void SetVertices(double radiusX, double radiusY, double height, int resolution)
        {
            double dAngle = 2.0 * Math.PI / resolution;

            var vertices = new List<IPoint>();

            for (int i = 0; i < resolution; i++)
            {
                double angle = i * dAngle;

                var cosa = Math.Cos(angle);
                var sina = Math.Sin(angle);

                double x = radiusX * cosa;
                double y = radiusY * sina;

                var vertex = m_pointCreator.Create(x, y, 0.0);
                vertices.Add(vertex);

                SetVertex(i, vertex.X, vertex.Y, 0.0);
                SetNormal(i, cosa, sina, 0.0);

                SetVertex(resolution + i, vertex.X, vertex.Y, height);
                SetNormal(resolution + i, cosa, sina, 0.0);
            }

            int index = 2 * resolution;
            for (int i = 0; i < resolution; i++)
            {
                var vertex = vertices[i];

                SetVertex(index, vertex.X, vertex.Y, 0.0);
                SetNormal(index++, 0.0, 0.0, -1.0);
            }

            for (int i = 0; i < resolution; i++)
            {
                var vertex = vertices[i];

                SetVertex(index, vertex.X, vertex.Y, height);
                SetNormal(index++, 0.0, 0.0, 1.0);
            }

            SetVertex(index, 0.0, 0.0, 0.0);
            SetNormal(index++, 0.0, 0.0, -1.0);

            SetVertex(index, 0.0, 0.0, height);
            SetNormal(index++, 0.0, 0.0, 1.0);
        }
    }
}

