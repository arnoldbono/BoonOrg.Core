// (c) 2019 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Commands.Responses;
using BoonOrg.Storage;

using BoonOrg.Geometry.Commands;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.CommandHandlers
{
    internal sealed class CommandHandlerNormalizeTrimesh : ICommandHandler<CommandNormalizeTrimesh, IResponse>
    {
        private readonly ITriangleContainerNormalizer m_normalizer;
        private readonly IGeometryLocatorService m_geometryLocatorService;
        private readonly IDocumentServer m_documentServer;

        public CommandHandlerNormalizeTrimesh(ITriangleContainerNormalizer normalizer,
            IGeometryLocatorService geometryLocatorService,
            IDocumentServer documentServer)
        {
            m_normalizer = normalizer;
            m_geometryLocatorService = geometryLocatorService;
            m_documentServer = documentServer;
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command)
        {
            return HandleCommand(commandExecuter, (CommandNormalizeTrimesh)command);
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, CommandNormalizeTrimesh command)
        {
            var document = m_documentServer.Document;

            var surface = m_geometryLocatorService.Find<ITriangleContainer>(command.Surface, document);
            return new Response { Success = surface != null && m_normalizer.Normalize(surface) };
        }
    }
}
