// (c) 2023 Roland Boon

using System;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Persistence
{
    /// <summary>
    /// General interface for trimesh exporters.
    /// </summary>
    public interface ITrimeshExporter
    {
        /// <summary>
        /// The extension of the data file of the type of export. Like <code>@".usda"</code>.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Export the trimeshes that reference the materials to the file with the given path.
        /// </summary>
        /// <param name="trimeshes">The surfaces to export</param>
        /// <param name="materials">The materials referenced by the surfaces.</param>
        /// <param name="path">The path to the file (will overwrite any existing file).</param>
        /// <param name="includeNormals">Include the normal vectors in the export as well?/</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Export(ITrimesh[] trimeshes, IMaterial[] materials, string path, bool includeNormals);
    }
}
