// (c) 2017, 2019 Roland Boon

using BoonOrg.Storage;

namespace BoonOrg.Geometry.Logic
{
    public interface ISurfaceRenamer
    {
        /// <summary>
        /// Rename the given surface <code cref="surface"/> using the name <code cref="name"/>.
        /// </summary    
        /// <param name="surface">The surface to rename.</param>
        /// <param name="name">The new name of the surface</param>
        /// <param name="document">The document that contains the surface</param>
        void Execute(ISurface surface, string name, IDocument document);
    }
}
