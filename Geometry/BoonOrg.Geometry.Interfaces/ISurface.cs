// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    /// <summary>
    /// A surface has nodes. Think of a triangular mesh or a 2D grid.
    /// Sometimes the data is very sparse while at other times it is very dense.
    /// Mathematically, you can describe a surface that is infinite. But we will limit
    /// this implementation to finite geometries, i.e. having a bounding box. 
    /// </summary>
    public interface ISurface : IGeometry
    {
    }
}
