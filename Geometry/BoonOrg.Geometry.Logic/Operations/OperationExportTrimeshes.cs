// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BoonOrg.Commands;
using BoonOrg.DrawingTools.Textures;
using BoonOrg.Functions;
using BoonOrg.Functions.Domain;
using BoonOrg.Identification;
using BoonOrg.Storage;

using BoonOrg.Geometry.Persistence;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Logic.Operations
{
    internal sealed class OperationExportTrimeshes : Operation, IExecuteWithCommandExecuter
    {
        private readonly ITrimeshExporterFactory m_trimeshExporterFactory;
        private readonly IDocumentServer m_documentServer;
        private readonly Func<IMaterial> m_materialFunc;

        public OperationExportTrimeshes(IParameterCollection parameters,
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
            if (!Parameters.GetPath(@"uri", out var uri))
            {
                return false;
            }

            // Optional parameter
            Parameters.Get(@"trimesh", container, out string[] trimeshNames);
            Parameters.GetBool(@"includeNormals", out var includeNormals);
            Parameters.GetString(@"exporter", out var selectedExporter);
            if (string.IsNullOrEmpty(selectedExporter) && uri.Contains('.'))
            {
                selectedExporter = Path.GetExtension(uri).Substring(1).ToLower();
            }

            var geometryContainer = document.Get<IGeometryContainer>();
            if (geometryContainer == null)
            {
                return false;
            }

            var trimeshes = GetTrimeshes(trimeshNames, geometryContainer);
            if (trimeshes == null)
            {
                return false;
            }

            var materials = GetMaterials(trimeshes, container);

            var trimeshExporter = m_trimeshExporterFactory.Create(selectedExporter);
            return trimeshExporter.Export(trimeshes, materials, uri, includeNormals);
        }

        public static ITrimesh[] GetTrimeshes(string[] trimeshNames, IGeometryContainer geometryContainer)
        {
            var trimeshes = new List<ITrimesh>();
            if (trimeshNames == null || trimeshNames.Length == 0)
            {
                trimeshes.AddRange(geometryContainer.GetList<ITrimesh>());
            }
            else
            {
                foreach (var trimeshName in trimeshNames)
                {
                    var surface = geometryContainer.Get(trimeshName);
                    if (surface is ITrimesh trimesh)
                    {
                        trimeshes.Add(trimesh);
                    }
                    else if (surface is IInstancedShape instancedShape)
                    {
                        trimeshes.Add(instancedShape.Trimesh);
                    }
                }
            }

            return trimeshes.ToArray();
        }


        public IMaterial[] GetMaterials(IEnumerable<ITrimesh> trimeshes, IIdentifiableContainer container)
        {
            List<IMaterial> materials = [];

            foreach (var trimesh in trimeshes)
            {
                var material = container.FindByName<IMaterial>(trimesh.Material);
                if (material == null)
                {
                    material = m_materialFunc();
                    trimesh.Material = @$"{trimesh.Identification.Name}material";
                    material.Identification.Rename(trimesh.Material);
                    container.Add(material);
                }

                if (materials.Any(m => string.Compare(m.Identification.Name, material.Identification.Name, false) == 0))
                {
                    continue; // Skip if material already added
                }

                materials.Add(material);
            }

            return [.. materials];
        }


        public static IBitmapTexture[] GetTextures(IEnumerable<ITrimesh> trimeshes, IIdentifiableContainer container)
        {
            List<IBitmapTexture> textures = new();

            bool isTextureValidAndNotYetIncluded(string texture) =>
                !string.IsNullOrEmpty(texture) &&
                textures.Any(y => string.Compare(y.Identification.Name, texture, true) == 0);

            foreach (var trimeshTexture in trimeshes.Select(x => x.Texture).Where(isTextureValidAndNotYetIncluded))
            {
                var texture = container.FindByName<IBitmapTexture>(trimeshTexture);
                if (texture != null)
                {
                    // Only export a trimesh when it references (no or) a known texture
                    return null;
                }

                textures.Add(texture);
            }

            return textures.ToArray();
        }
    }
}
