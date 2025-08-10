// (c) 2017 Roland Boon

namespace BoonOrg.Geometry.Logic
{
    public interface ISurfaceTranslator<T> : ISurfaceTranslator where T : ISurface 
    {
        /// <summary>
        /// Translate the given surface <see cref="surface"/> with the given <see cref="translation"/>.
        /// </summary>
        /// <param name="surface">The surface to translate.</param>
        /// <param name="translation">The translation vector.</param>
        void Execute(T surface, IVector translation);
    }

    public interface ISurfaceTranslator
    {
        /// <summary>
        /// Translate the given surface <see cref="surface"/> with the given <see cref="translation"/>.
        /// </summary>
        /// <param name="surface">The surface to translate.</param>
        /// <param name="translation">The translation vector.</param>
        void Execute(ISurface surface, IVector translation);

        bool Supports(ISurface surface);
    }
}
