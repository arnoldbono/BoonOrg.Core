// (c) 2019 Roland Boon

using System;

using BoonOrg.Storage;
using BoonOrg.Commands;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class GeometryFinder : IGeometryFinder
    {
        private readonly IDocumentServer m_documentServer;

        public GeometryFinder(IDocumentServer documentServer)
        {
            m_documentServer = documentServer;
        }

        public IGeometry Find(string geometryName)
        {
            return Find(geometryName, m_documentServer.Document);
        }

        public IGeometry Find(string geometryName, IDocument document)
        {
            var geometryContainer = document?.Get<IGeometryContainer>();
            return geometryContainer?.Get(geometryName);
        }

        public IGeometry Find(Guid geometryId)
        {
            return Find(geometryId, m_documentServer.Document);
        }

        public IGeometry Find(Guid geometryId, IDocument document)
        {
            var geometryContainer = document?.Get<IGeometryContainer>();
            return geometryContainer?.Get(geometryId);
        }

        public string FindUniqueName(string name)
        {
            return FindUniqueName(name, m_documentServer.Document);
        }

        public string FindUniqueName(string name, IDocument document)
        {
            var geometryContainer = document?.Get<IGeometryContainer>();
            if (geometryContainer?.Get(name) == null)
            {
                return name;
            }

            string duplicateNameFormat = @"{0} - Copy";
            string duplicateName = name;
            for (int i = 1; i < 10; ++i)
            {
                duplicateName = string.Format(duplicateNameFormat, duplicateName);
                if (geometryContainer.Get(duplicateName) == null)
                {
                    return duplicateName;
                }
            }

            return string.Empty;
        }

    }
}
