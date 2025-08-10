// (c) 2015,2017,2019 Roland Boon

using BoonOrg.Storage;

using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class SurfaceRenamer : ISurfaceRenamer
    {
        private readonly IGeometryEventService m_geometryEventsService;

        public SurfaceRenamer(IGeometryEventService geometryEventsService)
        {
            m_geometryEventsService = geometryEventsService;
        }

        /// <inheritdoc/>
        public void Execute(ISurface surface, string name, IDocument document)
        {
            if (surface != null)
            {
                surface.Identification.Rename(name);

                m_geometryEventsService.Modified(surface, false, false, false, false);
            }
        }
    }
}
