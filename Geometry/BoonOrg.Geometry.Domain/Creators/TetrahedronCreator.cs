// (c) 2017, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class TetrahedronCreator : ITetrahedronCreator
    {
        private readonly Func<ITetrahedron> m_tetrahedronFunc;

        public TetrahedronCreator(Func<ITetrahedron> tetrahedronFunc)
        {
            m_tetrahedronFunc = tetrahedronFunc;
        }

        public ITetrahedron Create()
        {
            return m_tetrahedronFunc();
        }

        public ITetrahedron Create(IPoint v1, IPoint v2, IPoint v3, IPoint v4) =>
            Create(new List<IPoint> { v1, v2, v3, v4 });

        public ITetrahedron Create(IReadOnlyList<IPoint> vertices)
        {
            var tetrahedron = m_tetrahedronFunc();
            tetrahedron.Set(vertices);
            return tetrahedron;
        }
    }
}
