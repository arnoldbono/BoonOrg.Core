// (c) 2017, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class LayerGridCreator : ILayerGridCreator
    {
        private readonly Func<ILayerGrid> m_layerGridFunc;

        public LayerGridCreator(Func<ILayerGrid> layerGridFunc)
        {
            m_layerGridFunc = layerGridFunc;
        }

        public ILayerGrid Create()
        {
            return Create(0, 0, string.Empty);
        }

        public ILayerGrid Create(int rows, int columns, string name)
        {
            var grid = m_layerGridFunc();
            grid.Set(rows, columns, name);
            return grid;
        }

        public ILayerGrid Create(List<IPoint[]> rows, string name)
        {
            var grid = m_layerGridFunc();
            grid.Set(rows, name);
            return grid;
        }
    }
}
