// (c) 2017 Roland Boon

namespace BoonOrg.Geometry.Logic
{
    public interface ISurfaceCopy<T> : ISurfaceCopy where T : ISurface
    {
        /// <summary>
        /// Copy the given surface <paramref name="surface"/> and give it the name <paramref name="name"/>.
        /// </summary>
        /// <param name="surface">The surface to copy.</param>
        /// <param name="name">The name of the new surface</param>
        /// <returns>The copied surface</returns>
        T Execute(T surface, string name);
    }

    public interface ISurfaceCopy
    {
        /// <summary>
        /// Copy the given surface <paramref name="surface"/> and give it the name <paramref name="name"/>.
        /// </summary>
        /// <param name="surface">The surface to copy.</param>
        /// <param name="name">The name of the new surface</param>
        /// <returns>The copied surface</returns>
        ISurface Execute(ISurface surface, string name);

        /// <summary>
        /// Is the given surface <paramref name="surface"/> of the correct type?/>.
        /// </summary>
        /// <returns>True iff the surface is of the correct type.</returns>
        bool Supports(ISurface surface);
    }

    public interface ISurfaceCopier
    {
        /// <summary>
        /// Copy the given surface <paramref name="surface"/> and give it the name <paramref name="name"/>.
        /// </summary>
        /// <param name="surface">The surface to copy.</param>
        /// <param name="name">The name of the new surface</param>
        /// <returns>The copied surface or null if it failed.</returns>
        ISurface Execute(ISurface surface, string name);
    }
}
