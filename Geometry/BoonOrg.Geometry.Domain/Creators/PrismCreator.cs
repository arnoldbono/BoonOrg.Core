// (c) 2017, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class PrismCreator : IPrismCreator
    {
        private readonly Func<IPrism> m_prismFunc;

        public PrismCreator(Func<IPrism> prismFunc)
        {
            m_prismFunc = prismFunc;
        }

        public IPrism Create()
        {
            return m_prismFunc();
        }

        public IPrism Create(ITriangle sideA, ITriangle sideB) =>
            Create(new List<IPoint> { sideA.Vertex1, sideA.Vertex2, sideA.Vertex3,
                sideB.Vertex1, sideB.Vertex2, sideB.Vertex3 });

        public IPrism Create(IPoint v1, IPoint v2, IPoint v3, IPoint v4, IPoint v5, IPoint v6) =>
            Create(new List<IPoint> { v1, v2, v3, v4, v5, v6 });

        public IPrism Create(IReadOnlyList<IPoint> vertices)
        {
            var prism = m_prismFunc();
            prism.Set(vertices);
            return prism;
        }
    }
}
