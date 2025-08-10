// (c) 2017, 2023 Roland Boon

using System;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class PyramidCreator : IPyramidCreator
    {
        private readonly Func<IPyramid> m_pyramidFunc;

        public PyramidCreator(Func<IPyramid> pyramidFunc)
        {
            m_pyramidFunc = pyramidFunc;
        }

        public IPyramid Create() => Create(1.0);

        public IPyramid Create(double size)
        {
            var pyramid = m_pyramidFunc();
            pyramid.Set(size);
            return pyramid;
        }

        public IPyramid Create(double width, double length, double height)
        {
            var pyramid = m_pyramidFunc();
            pyramid.Set(width, length, height);
            return pyramid;
        }
    }
}
