// (c) 2019 Roland Boon

using System.Collections.Generic;

using BoonOrg.Storage;
using BoonOrg.Commands;

using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class GeometryAdder : IGeometryAdder
    {
        private readonly IGeometryEventService m_geometryEventsService;
        private readonly IDocumentServer m_documentServer;

        public GeometryAdder(IGeometryEventService geometryEventsService, IDocumentServer documentServer)
        {
            m_geometryEventsService = geometryEventsService;
            m_documentServer = documentServer;
        }

        public void Add(IGeometry geometry)
        {
            Add(geometry, m_documentServer.Document);
        }

        public void Add(IGeometry geometry, IDocument document)
        {
            Add(new IGeometry[] { geometry }, document);
        }

        public void Add(IEnumerable<IGeometry> geometries)
        {
            Add(geometries, m_documentServer.Document);
        }

        public void Add(IEnumerable<IGeometry> geometries, IDocument document)
        {
            string[] typeIdentifiers = document.AllTypeIdentifiers;
            foreach (string typeIdentifier in typeIdentifiers)
            {
                if (document.Get(typeIdentifier) is IGeometryOwner geometryOwner)
                {
                    foreach (var geometry in geometries)
                    {
                        geometryOwner.AddGeometry(geometry);
                    }
                }
            }

            m_geometryEventsService.Added(geometries);
        }
    }
}
