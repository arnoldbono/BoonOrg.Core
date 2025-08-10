// (c) 2015, 2017, 2023 Roland Boon

using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class TrimeshTranslator : ISurfaceTranslator<ITrimesh>
    {
        private readonly IGeometryEventService m_geometryEventService;

        public TrimeshTranslator(IGeometryEventService geometryEventService)
        {
            m_geometryEventService = geometryEventService;
        }

        /// <inheritdoc/>
        public void Execute(ITrimesh surface, IVector translation)
        {
            foreach (IPoint vertex in surface.Vertices)
            {
                vertex.X += translation.X;
                vertex.Y += translation.Y;
                vertex.Z += translation.Z;
            }

            m_geometryEventService.Modified(surface, true, true, false, false);
        }

        /// <inheritdoc/>
        public void Execute(ISurface surface, IVector translation)
        {
            Execute((ITrimesh)surface, translation);
        }

        /// <inheritdoc/>
        public bool Supports(ISurface surface) => surface is ITrimesh;
    }
}
