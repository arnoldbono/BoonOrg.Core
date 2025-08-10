// (c) 2017 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class TriangleCreator : ITriangleCreator
    {
        private readonly Func<ITriangle> m_triangleFunc;

        public TriangleCreator(Func<ITriangle> triangleFunc)
        {
            m_triangleFunc = triangleFunc;
        }

        public ITriangle Create() => m_triangleFunc();

        public ITriangle Create(IPoint v1, IPoint v2, IPoint v3)
        {
            var triangle = m_triangleFunc();
            triangle.Assign(v1, v2, v3);
            return triangle;
        }

        public ITriangle Create(IReadOnlyList<IPoint> vertices)
        {
            var triangle = m_triangleFunc();
            triangle.Assign(vertices);
            return triangle;
        }
    }
}
