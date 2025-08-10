// (c) 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Commands;
using BoonOrg.Functions;
using BoonOrg.Functions.Domain;
using BoonOrg.Identification;
using BoonOrg.Storage;

using BoonOrg.Geometry.Persistence;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Logic.Operations
{
    internal sealed class OperationExportTrimesh : Operation, IExecuteWithCommandExecuter
    {
        private readonly ITrimeshExporterFactory m_trimeshExporterFactory;
        private readonly IDocumentServer m_documentServer;
        private readonly Func<IMaterial> m_materialFunc;

        public OperationExportTrimesh(IParameterCollection parameters,
            ITrimeshExporterFactory trimeshExporterFactory,
            IDocumentServer documentServer,
            Func<IMaterial> materialFunc) : base(parameters)
        {
            m_trimeshExporterFactory = trimeshExporterFactory;
            m_documentServer = documentServer;
            m_materialFunc = materialFunc;
        }

        public bool Execute(ICommandExecuter commandExecuter)
        {
            var document = m_documentServer.Document;
            var container = document.Get<IIdentifiableContainer>();

            // Mandatory parameters
            if (!Parameters.GetString(@"trimesh", out var trimeshName) ||
                !Parameters.GetPath(@"uri", out var uri))
            {
                return false;
            }

            // Optional parameter
            Parameters.GetBool(@"includeNormals", out var includeNormals);
            Parameters.GetString(@"exporter", out var selectedExporter);
            if (string.IsNullOrEmpty(selectedExporter) && uri.Contains('.'))
            {
                selectedExporter = Path.GetExtension(uri).Substring(1).ToLower();
            }
            
            var geometryContainer = document.Get<IGeometryContainer>();
            if (geometryContainer?.Get(trimeshName) is not ITrimesh trimesh)
            {
                return false;
            }

            var material = container.FindByName<IMaterial>(trimesh.Material);
            if (material == null)
            {
                material = m_materialFunc();
                trimesh.Material = @$"{trimeshName}material";
                material.Identification.Rename(trimesh.Material);
                container.Add(material);
            }
            var trimeshExporter = m_trimeshExporterFactory.Create(selectedExporter);
            return trimeshExporter.Export([trimesh], [material], uri, includeNormals);
        }
    }
}
