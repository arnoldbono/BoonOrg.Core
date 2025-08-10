// (c) 2017 Roland Boon
//------------------------------------------------------------------
// (c) 2009 Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------
// https://www.codeproject.com/Articles/42174/High-performance-WPF-D-Chart
//------------------------------------------------------------------
// Cone is a reworked version of Cone3D.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Logic;
using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <summary>
    /// A cone.
    /// </summary>
    internal sealed class Cone : TriangleContainer, ICone
    {
        private readonly IVectorCreator m_vectorCreator;
        private readonly IPointCreator m_pointCreator;
        private readonly IVectorLogic m_vectorLogic;

        public double RadiusX { get; private set; }

        public double RadiusY { get; private set; }

        public double Height { get; private set; }

        public int Resolution { get; private set; }

        public Cone(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc,
            IVectorCreator vectorCreator, IPointCreator pointCreator, IVectorLogic vectorLogic) :
            base(identity, boundaryBoxFunc)
        {
            Set(1.0, 1.0, 1.0, 12);
            m_vectorCreator = vectorCreator;
            m_pointCreator = pointCreator;
            m_vectorLogic = vectorLogic;
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

            int vertexCount = 3 * Resolution + 2;
            int triangleCount = 2 * Resolution;

            SetCapacity(vertexCount, triangleCount);
            SetTriangles();
            SetVertices();

            return this;
        }

        private void SetTriangles()
        {
            var index = 0;

            // Sides
            for (int i = 0; i < Resolution - 1; ++i)
            {
                SetTriangle(index++, i, i + 1, Resolution + 1 + i);
            }

            SetTriangle(index++, Resolution - 1, 0, 2 * Resolution - 1);

            // Base disk
            var offset = 2 * Resolution;
            for (int i = 0; i < Resolution - 1; ++i)
            {
                SetTriangle(index++, offset + i + 1, offset + i, offset + Resolution); // disk
            }

            SetTriangle(index++, offset, offset + Resolution - 1, offset + Resolution); // disk
        }

        private void SetVertices()
        {
            double dAngle = 2.0 * Math.PI / Resolution;

            var vertices = new List<IPoint>();
            var normals = new List<IVector>();

            // Cone base disk vertices
            for (int i = 0; i < Resolution; ++i)
            {
                var angle = i * dAngle;

                var cosa = Math.Cos(angle);
                var sina = Math.Sin(angle);
                var x = RadiusX * cosa;
                var y = RadiusY * sina;
                var length = Math.Sqrt(x * x + y * y);

                var sideLength = Math.Sqrt(Height * Height + length * length);
                var nx = length / sideLength;
                var ny = Height / sideLength;
                
                var vertex = m_pointCreator.Create(x, y, 0.0);
                vertices.Add(vertex);

                var normal = m_vectorCreator.Create(nx, ny * cosa, ny * sina);
                normal.Normalize();
                normals.Add(normal);

                SetVertex(i, vertex.X, vertex.Y, vertex.Z);
                SetNormal(i, normal.X, normal.Y, normal.Z);
            }

            // Cone top. Same vertex, different normal. Normal needs to be average of the two other normals on the triangle.
            var index = Resolution;
            for (int i = 0; i < Resolution; ++i, ++index)
            {
                var normal = m_vectorLogic.Weighed(normals[i], normals[i + 1 == Resolution ? 0 : i + 1], 0.5, 0.5);
                normal.Normalize();

                SetVertex(index, 0.0, 0.0, Height);
                SetNormal(index, normal.X, normal.Y, normal.Z); 
            }

            // Cone base disk vertices
            for (int i = 0; i < Resolution; ++i, ++index)
            {
                var vertex = vertices[i];

                SetVertex(index, vertex.X, vertex.Y, vertex.Z);
                SetNormal(index, 0.0, 0.0, -1.0); // Normal disk
            }

            // Cone's disk center node
            SetVertex(index, 0.0, 0.0, 0.0);
            SetNormal(index, 0.0, 0.0, -1.0);
        }
    }
}
