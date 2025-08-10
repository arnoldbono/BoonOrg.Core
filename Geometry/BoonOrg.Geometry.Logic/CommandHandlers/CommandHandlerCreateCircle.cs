// (c) 2020 Roland Boon

using System;

using BoonOrg.Commands;
using BoonOrg.Commands.Responses;
using BoonOrg.Storage;

using BoonOrg.Geometry.Commands;

namespace BoonOrg.Geometry.Logic.CommandHandlers
{
    internal sealed class CommandHandlerCreateCircle : ICommandHandler<CommandCreateCircle, IResponse>
    {
        private readonly Func<ICircle> m_circleCreator;
        private readonly IGeometryDeleter m_surfaceDeleter;
        private readonly IGeometryAdder m_surfaceAdder;
        private readonly IDocumentServer m_documentServer;

        public CommandHandlerCreateCircle(Func<ICircle> circleCreator,
            IGeometryDeleter surfaceDeleter,
            IGeometryAdder surfaceAdder,
            IDocumentServer documentServer)
        {
            m_circleCreator = circleCreator;
            m_surfaceDeleter = surfaceDeleter;
            m_surfaceAdder = surfaceAdder;
            m_documentServer = documentServer;
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command)
        {
            return HandleCommand(commandExecuter, (CommandCreateCircle)command);
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, CommandCreateCircle command)
        {
            var document = m_documentServer.Document;

            m_surfaceDeleter.Delete(command.Name);

            var circle = m_circleCreator();
            circle.Set(command.Center, command.Normal, command.Name);
            circle.Radius = command.Radius;
            circle.Count = command.Count;

            m_surfaceAdder.Add(circle, document);

            return new Response { Success = true };
        }
    }
}
