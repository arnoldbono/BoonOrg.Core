// (c) 2018, 2023 Roland Boon

using System;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class TriangleContainerCreator : ITriangleContainerCreator
    {
        private readonly Func<ITriangleContainer> m_triangleContainerFunc;

        public TriangleContainerCreator(Func<ITriangleContainer> triangleContainerFunc)
        {
            m_triangleContainerFunc = triangleContainerFunc;
        }

        public ITriangleContainer Create() => m_triangleContainerFunc();
    }
}
