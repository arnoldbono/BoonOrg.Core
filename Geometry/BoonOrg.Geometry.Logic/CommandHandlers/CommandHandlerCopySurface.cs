// (c) 2017, 2019 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Commands.Responses;
using BoonOrg.Storage;

using BoonOrg.Geometry.Commands;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.CommandHandlers
{
    internal sealed class CommandHandlerCopySurface : ICommandHandler<CommandCopySurface, IResponse>
    {
        private readonly IGeometryDeleter m_geometryDeleter;
        private readonly IGeometryAdder m_geometryAdder;
        private readonly ISurfaceCopier m_surfaceCopier;
        private readonly IGeometryLocatorService m_geometryLocatorService;
        private readonly IDocumentServer m_doumentServer;

        public CommandHandlerCopySurface(IGeometryDeleter surfaceDeleter,
            IGeometryAdder surfaceAdder,
            ISurfaceCopier surfaceCopier,
            IGeometryLocatorService geometryLocatorService,
            IDocumentServer documentServer)
        {
            m_geometryDeleter = surfaceDeleter;
            m_geometryAdder = surfaceAdder;
            m_surfaceCopier = surfaceCopier;
            m_geometryLocatorService = geometryLocatorService;
            m_doumentServer = documentServer;
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command)
        {
            return HandleCommand(commandExecuter, (CommandCopySurface)command);
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, CommandCopySurface command)
        {
            var document = m_doumentServer.Document;

            var sourceSurface = m_geometryLocatorService.Find<ISurface>(command.SourceSurface, document);
            if (sourceSurface == null)
            {
                return new Response { Success = false };
            }

            string sourceName = sourceSurface.Identification.Name;
            string targetName = command.TargetSurface;
            bool newName = string.IsNullOrEmpty(targetName);
            if (newName)
            {
                targetName = $@"{sourceName} - Copy";
            }

            var copiedSurface = m_surfaceCopier.Execute(sourceSurface, targetName);
            if (copiedSurface == null)
            {
                return new Response { Success = false };
            }

            m_geometryDeleter.Delete(targetName);

            m_geometryAdder.Add(copiedSurface, document);

            return new Response { Success = true };
        }
    }
}
