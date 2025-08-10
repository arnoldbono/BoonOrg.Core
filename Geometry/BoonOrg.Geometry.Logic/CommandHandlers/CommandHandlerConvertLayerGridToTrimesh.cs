// (c) 2017, 2019 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Commands.Responses;
using BoonOrg.Storage;

using BoonOrg.Geometry.Commands;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.CommandHandlers
{
    internal sealed class CommandHandlerConvertLayerGridToTrimesh : ICommandHandler<CommandConvertLayerGridToTrimesh, IResponse>
    {
        private readonly IGeometryFinder m_finder;
        private readonly IGeometryDeleter m_deleter;
        private readonly IGeometryAdder m_adder;
        private readonly ILayerGridToTrimeshTransformer m_transformer;
        private readonly IDocumentServer m_documentServer;

        public CommandHandlerConvertLayerGridToTrimesh(ILayerGridToTrimeshTransformer transformer,
            IGeometryFinder finder,
            IGeometryDeleter deleter,
            IGeometryAdder adder,
            IDocumentServer documentServer)
        {
            m_finder = finder;
            m_deleter = deleter;
            m_adder = adder;
            m_transformer = transformer;
            m_documentServer = documentServer;
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command)
        {
            return HandleCommand(commandExecuter, (CommandConvertLayerGridToTrimesh)command);
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, CommandConvertLayerGridToTrimesh command)
        {
            string sourceName = command.SourceSurface;
            string targetName = command.TargetSurface;
            bool targetNameEmpty = string.IsNullOrEmpty(targetName);
            bool replace = !targetNameEmpty && string.Compare(sourceName, targetName, true) == 0;
            if (replace)
            {
                targetName = sourceName;
            }
            else if (targetNameEmpty)
            {
                targetName = $@"{sourceName} - Trimesh";
            }

            var document = m_documentServer.Document;

            if (m_finder.Find(sourceName, document) is not ILayerGrid sourceSurface)
            {
                return new Response { Success = false };
            }

            var trimesh = m_transformer.Transform(sourceSurface, targetName);

            m_deleter.Delete(targetName);
            m_adder.Add(trimesh, document);

            return new Response { Success = true };
        }
    }
}
