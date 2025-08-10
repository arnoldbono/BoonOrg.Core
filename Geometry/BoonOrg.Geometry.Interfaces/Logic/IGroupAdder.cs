// (c) 2019 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Storage;

namespace BoonOrg.Geometry.Logic
{
    public interface IGroupAdder
    {
        /// <summary>
        /// Create the group and add it to the goup specified by the given path.
        /// </summary>
        /// <param name="document">The document to find the parent group in.</param>
        /// <param name="name">The name of the new goup</param>
        /// <param name="path">The path of the group where the new goup will be added to.</param>
        /// <returns>The created group.</returns>
        /// <remarks>Use "/" to create a top-level group.</remarks>
        IIdentifiableContainer Execute(IDocument document, string name, string path);
    }
}
