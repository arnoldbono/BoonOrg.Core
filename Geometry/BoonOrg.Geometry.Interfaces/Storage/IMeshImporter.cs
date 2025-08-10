// (c) 2017 Roland Boon

namespace BoonOrg.Geometry.Storage
{
    public interface IMeshImporter
    {
        /// <summary>
        /// Get the trimesh from a data file.
        /// </summary>
        /// <param name="path">Path to the data file.</param>
        /// <param name="name">The name of the surface.</param>
        /// <returns>the trimesh</returns>
        ITrimesh Import(string path, string name);
    }
}
