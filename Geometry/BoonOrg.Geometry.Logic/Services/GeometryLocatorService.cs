// (c) 2019 Roland Boon

using System;

using BoonOrg.Commands;
using BoonOrg.Storage;

using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.Services
{
    internal sealed class GeometryLocatorService : IGeometryLocatorService
    {
        private readonly IDocumentServer m_documentServer;

        public GeometryLocatorService(IDocumentServer documentServer)
        {
            m_documentServer = documentServer;
        }

        public T Find<T>(string name, IDocument document = null) where T : class, IGeometry
        {
            if (document == null)
            {
                document = m_documentServer.Document;
            }

            var geometryContainer = document.Get<IGeometryContainer>();
            return geometryContainer.Get(name) as T;
        }

        public T Find<T>(Guid id, IDocument document = null) where T : class, IGeometry
        {
            if (document == null)
            {
                document = m_documentServer.Document;
            }

            var geometryContainer = document.Get<IGeometryContainer>();
            return geometryContainer?.Get(id) as T;
        }
    }
}
