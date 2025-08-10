// (c) 2017, 2019 Roland Boon

using System.Collections.Generic;

using BoonOrg.Commands;
using BoonOrg.Commands.Responses;
using BoonOrg.Storage;

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Commands;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.CommandHandlers
{
    internal sealed class CommandHandlerDisplaceSurface : ICommandHandler<CommandDisplaceSurface, IResponse>
    {
        private readonly IVectorCreator m_vectorCreator;
        private readonly IEnumerable<ISurfaceTranslator> m_translators;
        private readonly IGeometryLocatorService m_geometryLocatorService;
        private readonly IDocumentServer m_documentServer;

        public CommandHandlerDisplaceSurface(IVectorCreator vectorCreator,
            IEnumerable<ISurfaceTranslator> translators,
            IGeometryLocatorService geometryLocatorService,
            IDocumentServer documentServer)
        {
            m_vectorCreator = vectorCreator;
            m_translators = translators;
            m_geometryLocatorService = geometryLocatorService;
            m_documentServer = documentServer;
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command)
        {
            return HandleCommand(commandExecuter, (CommandDisplaceSurface)command);
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, CommandDisplaceSurface command)
        {
            var document = m_documentServer.Document;

            var surface = m_geometryLocatorService.Find<ISurface>(command.SurfaceName, document);
            if (surface == null)
            {
                return new Response { Success = false };
            }

            foreach (ISurfaceTranslator surfaceTranslator in m_translators)
            {
                if (!surfaceTranslator.Supports(surface))
                {
                    continue;
                }

                var displacement = m_vectorCreator.Create(command.OffsetX, command.OffsetY, command.OffsetZ);
                surfaceTranslator.Execute(surface, displacement);
                return new Response { Success = true };
            }

            return new Response { Success = false };
        }
    }
}
