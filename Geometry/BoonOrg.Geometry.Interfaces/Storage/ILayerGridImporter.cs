// (c) 2017 Roland Boon

using BoonOrg.Storage;
using BoonOrg.Units;

namespace BoonOrg.Geometry.Storage
{
    public interface ILayerGridImporter
    {
        /// <summary>
        /// Get the rectangular grid layer from a data file which is specified in the unit given.
        /// </summary>
        /// <param name="document">The document that will contain the data.</param>
        /// <param name="path">Path to the data file.</param>
        /// <param name="unit">The length unit of the numbers in the data file.</param>
        /// <param name="name">The name of the surface.</param>
        /// <returns>the rectangular grid layer </returns>
        ILayerGrid Import(IDocument document, string path, IUnit unit, string name);
    }
}
