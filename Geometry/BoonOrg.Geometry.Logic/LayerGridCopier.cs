// (c) 2015, 2017, 2019 Roland Boon

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class LayerGridCopier : ISurfaceCopy<ILayerGrid>
    {
        private readonly ILayerGridCreator m_gridCreator;

        public LayerGridCopier(ILayerGridCreator gridCreator)
        {
            m_gridCreator = gridCreator;
        }

        /// <inheritdoc/>
        public bool Supports(ISurface surface) => surface is ILayerGrid;

        /// <inheritdoc/>
        public ISurface Execute(ISurface surface, string name)
        {
            return Execute(surface as ILayerGrid, name);
        }

        /// <inheritdoc/>
        public ILayerGrid Execute(ILayerGrid surface, string name)
        {
            ILayerGrid copiedSurface = m_gridCreator.Create(surface.Rows, surface.Columns, name);
            int rowCount = surface.Rows;
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                int columnCount = surface.Columns;
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    IPoint node = surface.Nodes[rowIndex, columnIndex];
                    IPoint copiedNode = copiedSurface.Nodes[rowIndex, columnIndex];
                    copiedNode.X += node.X;
                    copiedNode.Y += node.Y;
                    copiedNode.Z += node.Z;
                }
            }
            return copiedSurface;
        }
    }
}
