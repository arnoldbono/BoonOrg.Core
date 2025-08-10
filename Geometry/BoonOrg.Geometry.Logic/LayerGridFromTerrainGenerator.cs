// (c) 2017 Roland Boon

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class LayerGridFromTerrainGenerator : ILayerGridFromTerrainGenerator
    {
        private readonly ILayerGridCreator m_gridCreator;

        public LayerGridFromTerrainGenerator(ILayerGridCreator gridCreator)
        {
            m_gridCreator = gridCreator;
        }

        public ILayerGrid Generate(ITerrain terrain, IArea area, int xs, int ys)
        {
            // Make a mesh to hold the surface.
            var grid = m_gridCreator.Create(xs + 1, ys + 1, @"Terrain");

            // Make the surface's points and triangles.
            double dx = area.ScaleX / xs;
            double dy = area.ScaleY / ys;

            double x = area.MinX;
            for (int ix = 0; ix <= xs; ++ix, x += dx)
            {
                double y = area.MinY;
                for (int iy = 0; iy <= ys; ++iy, y += dy)
                {
                    IPoint node = grid.Nodes[iy, ix];
                    node.X = x;
                    node.Y = y;
                    node.Z = terrain.F(x, y);
                }
            }

            return grid;
        }
    }
}
