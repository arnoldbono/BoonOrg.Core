// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    /// One layer with a grid like pattern. Simple rectangular, equidistant mesh.
    /// </summary>
    public interface ILayerGrid : ISurface
    {
        IPoint[,] Nodes { get; }

        int Columns { get; }

        int Rows { get; }

        IArea Area { get; }

        IEnumerable<IPoint> Points { get; }

        /// <summary>
        /// All four-node cells.
        /// </summary>
        IEnumerable<IPoint[]> Cells { get; }

        void Set(int rows, int columns, string name);

        void Set(List<IPoint[]> rows, string name);

        /// <summary>
        /// Correct the grid layer's X and Y values for the given grid cell size.
        /// </summary>
        /// <param name="gridCellSize">The grid cell size.</param>
        void InitializeXY(double gridCellSize);

        /// <summary>
        /// Correct the grid layer's X and Y values for the given area.
        /// </summary>
        /// <param name="area">The grid's area.</param>
        void InitializeXY(IArea area);
    }
}
