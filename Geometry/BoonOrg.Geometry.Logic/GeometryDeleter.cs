// (c) 2017, 2019 Roland Boon

using System;

using BoonOrg.Commands;
using BoonOrg.Storage;

using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class GeometryDeleter : IGeometryDeleter
    {
        private readonly IGeometryEventService m_geometryEventsService;
        private readonly IGeometryLocatorService m_geometryLocatorService;
        private readonly ICommandExecuter m_commandExecuter;
        private readonly IDocumentServer m_documentServer;

        public GeometryDeleter(IGeometryEventService geometryEventsService,
            IGeometryLocatorService geometryLocatorService,
            ICommandExecuter commandExecuter,
            IDocumentServer documentServer)
        {
            m_geometryEventsService = geometryEventsService;
            m_geometryLocatorService = geometryLocatorService;
            m_commandExecuter = commandExecuter;
            m_documentServer = documentServer;
        }

        public void Delete(IGeometry geometry)
        {
            var document = m_documentServer.Document;
            string[] typeIdentifiers = document.AllTypeIdentifiers;
            foreach (string typeIdentifier in typeIdentifiers)
            {
                if (document.Get(typeIdentifier) is IGeometryOwner geometryOwner)
                {
                    geometryOwner.RemoveGeometry(geometry);
                }
            }

            m_geometryEventsService.Deleted(geometry);
        }

        public void Delete(Guid geometryId)
        {
            var document = m_documentServer.Document;
            var geometry = m_geometryLocatorService.Find<IGeometry>(geometryId, document);
            if (geometry != null)
            {
                Delete(geometry);
            }
        }

        public void Delete(string geometryName)
        {
            var document = m_documentServer.Document;
            var geometry = m_geometryLocatorService.Find<IGeometry>(geometryName, document);
            if (geometry != null)
            {
                Delete(geometry);
            }
        }

    }
}
