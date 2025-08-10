// (c) 2017, 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Storage;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Storage
{
    internal sealed class LayerGridStorageProvider : StorageProvider<ILayerGrid>
    {
        private readonly ILayerGridCreator m_gridCreator;
        private readonly IAreaCreator m_areaCreator;

        public LayerGridStorageProvider(Func<ILayerGrid> func,
            IStorageIdentifier storageIdentifier,
            ILayerGridCreator gridCreator,
            IAreaCreator areaCreator) : base(func, storageIdentifier)
        {
            m_gridCreator = gridCreator;
            m_areaCreator = areaCreator;
        }

        public override void ReadContent(BinaryReader reader, IDocument document, ILayerGrid grid)
        {
            int rows = reader.ReadInt32();
            int columns = reader.ReadInt32();

            grid.Set(rows, columns, grid.Identification.Name);

            double minX = reader.ReadDouble();
            double minY = reader.ReadDouble();
            double maxX = reader.ReadDouble();
            double maxY = reader.ReadDouble();

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    IPoint node = grid.Nodes[row, column];
                    node.Z = reader.ReadDouble();
                }
            }

            grid.InitializeXY(m_areaCreator.Create(minX, maxX, minY, maxY));
        }

        public override void WriteContent(ILayerGrid grid, BinaryWriter writer)
        {
            int rows = grid.Rows;
            int columns = grid.Columns;

            writer.Write(rows);
            writer.Write(columns);

            IArea area = grid.Area;
            writer.Write(area.MinX);
            writer.Write(area.MinY);
            writer.Write(area.MaxX);
            writer.Write(area.MaxY);

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    IPoint node = grid.Nodes[row, column];
                    writer.Write(node.Z);
                }
            }
        }
    }
}
