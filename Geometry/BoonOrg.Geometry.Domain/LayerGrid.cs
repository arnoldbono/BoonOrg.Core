// (c) 2015-2017 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <inheritdoc/>
    internal sealed class LayerGrid : Surface, ILayerGrid
    {
        private IPoint[,] m_nodes = new IPoint[0, 0];

        public IPoint[,] Nodes
        {
            get
            {
                return m_nodes;
            }
        }

        public int Columns { get; private set; }

        public int Rows { get; private set; }

        public IArea Area
        {
            get
            {
                if (Columns < 2 || Rows < 2)
                {
                    return null;
                }
                var area = new Area(Nodes[0, 0].X, Nodes[0, Columns - 1].X, Nodes[0, 0].Y, Nodes[Rows - 1, 0].Y);
                return area;
            }
        }

        public IEnumerable<IPoint> Points
        {
            get
            {
                int rowCount = Rows;
                int columnCount = Columns;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                    {
                        yield return Nodes[rowIndex, columnIndex];
                    }
                }
            }
        }

        public IEnumerable<IPoint[]> Cells
        {
            get
            {
                int rowCount = Rows;
                int columnCount = Columns;

                for (int rowIndex = 1; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 1; columnIndex < columnCount; columnIndex++)
                    {
                        // Get a polyline that forms a square when closed.
                        yield return new IPoint[]
                        {
                            // Make points at the corners of the surface
                            // over (x, y) - (x + dx, y + dy).
                            Nodes[rowIndex - 1, columnIndex - 1],
                            Nodes[rowIndex, columnIndex - 1],
                            Nodes[rowIndex, columnIndex],
                            Nodes[rowIndex - 1, columnIndex]
                        };
                    };
                }
            }
        }

        public LayerGrid(IIdentity identity) : base(identity)
        {
        }

        public void Set(int rows, int columns, string name)
        {
            Identification.Rename(name);
            Identification.ResetId(Guid.NewGuid());

            Rows = rows;
            Columns = columns;
            m_nodes = new Point[rows, columns];
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    Nodes[r, c] = new Point();
                }
            }
        }

        public void Set(List<IPoint[]> rows, string name)
        {
            Identification.Rename(name);
            Identification.ResetId(Guid.NewGuid());

            if (rows == null || !rows.Any() || rows.Any(r => r == null))
            {
                throw new ArgumentNullException(nameof(rows));
            }

            Rows = rows.Count;
            Columns = rows[0].Length;
            m_nodes = new IPoint[Rows, Columns];
            for (int r = 0; r < Rows; r++)
            {
                IPoint[] row = rows[r];
                if (row.Length != Columns)
                {
                    throw new ArgumentOutOfRangeException(nameof(rows), @"Incorrect number of columns in row{r}");
                }
                for (int c = 0; c < Columns; c++)
                {
                    Nodes[r, c] = row[c];
                }
            }
        }

        /// <inheritdoc/>
        public void InitializeXY(double gridCellSize)
        {
            int columnCount = Columns;
            int rowCount = Rows;

            double offsetY = (rowCount - 1) * 0.5;
            double offsetX = (columnCount - 1) * 0.5;
            for (int r = 0; r < rowCount; r++)
            {
                double y = (r - offsetY) * gridCellSize;

                for (int c = 0; c < columnCount; c++)
                {
                    double x = (c - offsetX) * gridCellSize;

                    IPoint node = Nodes[r, c];
                    node.X = x;
                    node.Y = y;
                }
            }
        }

        /// <inheritdoc/>
        public void InitializeXY(IArea area)
        {
            int columnCount = Columns;
            int rowCount = Rows;

            double minX = area.MinX;
            double minY = area.MinY;
            double offsetX = (columnCount < 2) ? 0.0 : area.ScaleX / (columnCount - 1);
            double offsetY = (rowCount < 2) ? 0.0 : area.ScaleY / (rowCount - 1);

            for (int r = 0; r < rowCount; r++)
            {
                double y = minY + r * offsetY;

                for (int c = 0; c < columnCount; c++)
                {
                    double x = minX + c * offsetX;

                    IPoint node = Nodes[r, c];
                    node.X = x;
                    node.Y = y;
                }
            }
        }

        private void GetExtrema(out double minZ, out double maxZ)
        {
            minZ = double.MaxValue;
            maxZ = double.MinValue;

            foreach (IPoint c in Points)
            {
                double z = c.Z;
                if (maxZ < z)
                {
                    maxZ = z;
                }
                if (minZ > z)
                {
                    minZ = z;
                }
            }
        }

        public override IBoundingBox GetBoundingBox()
        {
            GetExtrema(out double minZ, out double maxZ);

            return new BoundingBox
            {
                MinX = Nodes[0, 0].X,
                MaxX = Nodes[Rows - 1, Columns - 1].X,
                MinY = Nodes[0, 0].Y,
                MaxY = Nodes[Rows - 1, Columns - 1].Y,
                MinZ = minZ,
                MaxZ = maxZ
            };
        }

        public override void ExpandBoundingBox(IBoundingBox box)
        {
            box.Expand(GetBoundingBox());
        }
    }
}
