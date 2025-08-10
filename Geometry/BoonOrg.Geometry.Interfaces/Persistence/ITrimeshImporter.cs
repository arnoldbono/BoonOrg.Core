// (c) 2024 Roland Boon

using System;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Persistence
{
    public interface ITrimeshImporter
    {
        /// <summary>
        /// Specifies the file name extension of the supported file type.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// After import, contains the imported trimeshes.
        /// </summary>
        ITrimesh[] Trimeshes { get; }

        /// <summary>
        /// After import, contains the imported materials.
        /// </summary>
        IMaterial[] Materials { get; }

        bool Import(string path);
    }
}
