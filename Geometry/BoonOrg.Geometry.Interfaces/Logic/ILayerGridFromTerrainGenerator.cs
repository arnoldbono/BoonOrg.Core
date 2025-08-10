// (c) 2017 Roland Boon

namespace BoonOrg.Geometry.Logic
{
    public interface ILayerGridFromTerrainGenerator
    {
        ILayerGrid Generate(ITerrain terrain, IArea area, int rows, int columns);
    }
}
