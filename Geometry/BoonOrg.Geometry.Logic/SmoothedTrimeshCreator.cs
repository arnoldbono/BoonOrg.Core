// (c) 2017, 2023 Roland Boon

using System;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class SmoothedTrimeshCreator : ISmoothedTrimeshCreator
    {
        private readonly ITrimeshCreator m_trimeshCreator;
        private readonly IVectorLogic m_vectorLogic;
        private readonly Func<IBoundingBox> m_boundingBoxFunc;

        public SmoothedTrimeshCreator(ITrimeshCreator trimeshCreator, IVectorLogic vectorLogic, Func<IBoundingBox> boundingBoxFunc)
        {
            m_trimeshCreator = trimeshCreator;
            m_vectorLogic = vectorLogic;
            m_boundingBoxFunc = boundingBoxFunc;
        }

        public ITrimesh Create(ITriangle[] triangles, string name)
        {
            if (!triangles.Any())
            {
                return m_trimeshCreator.Create(triangles, name);
            }

            var boundingBox = m_boundingBoxFunc();

            foreach (var triangle in triangles)
            {
                boundingBox.Expand(triangle.Vertices);
            }

            var accuracy = 0.01 * boundingBox.Scale / triangles.Length;

            var vertices = new List<IPoint>();
            var triangleIndices = new List<int>();

            var count = 0;

            foreach (var triangle in triangles)
            {
                foreach (var vertex in triangle.Vertices)
                {
                    var found = false;
                    for (var i = 0; i < count; ++i)
                    {
                        if (m_vectorLogic.ComputeLength(vertices[i], vertex) < accuracy)
                        {
                            found = true;
                            triangleIndices.Add(i);
                            break;
                        }
                    }

                    if (!found)
                    {
                        triangleIndices.Add(count++);
                        vertices.Add(vertex);
                    }
                }
            }

            return m_trimeshCreator.Create(triangleIndices, vertices, name);
        }
    }
}
