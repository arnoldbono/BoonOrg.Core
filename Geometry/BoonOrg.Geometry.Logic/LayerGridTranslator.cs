// (c) 2015, 2017, 2023 Roland Boon

using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class LayerGridTranslator : ISurfaceTranslator<ILayerGrid>
    {
        private readonly IGeometryEventService m_geometryEventService;

        public LayerGridTranslator(IGeometryEventService geometryEventService)
        {
            m_geometryEventService = geometryEventService;
        }

        /// <inheritdoc/>
        public void Execute(ILayerGrid surface, IVector translation)
        {
            foreach (IPoint coordinate in surface.Points)
            {
                coordinate.X += translation.X;
                coordinate.Y += translation.Y;
                coordinate.Z += translation.Z;
            }

            m_geometryEventService.Modified(surface, true, true, false, false);
        }

        /// <inheritdoc/>
        public void Execute(ISurface surface, IVector translation)
        {
            Execute((ILayerGrid)surface, translation);
        }

        /// <inheritdoc/>
        public bool Supports(ISurface surface) => surface is ILayerGrid;
    }
}
