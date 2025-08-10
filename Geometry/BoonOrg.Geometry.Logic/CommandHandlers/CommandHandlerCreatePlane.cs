// (c) 2017, 2019 Roland Boon

using System;

using BoonOrg.Commands;
using BoonOrg.Commands.Responses;
using BoonOrg.Storage;

using BoonOrg.Geometry.Commands;

namespace BoonOrg.Geometry.Logic.CommandHandlers
{
    internal sealed class CommandHandlerCreatePlane : ICommandHandler<CommandCreatePlane, IResponse>
    {
        private readonly Func<IPlane> m_planeCreator;
        private readonly IGeometryDeleter m_surfaceDeleter;
        private readonly IGeometryAdder m_surfaceAdder;
        private readonly IDocumentServer m_documentServer;

        public CommandHandlerCreatePlane(Func<IPlane> planeCreator,
            IGeometryDeleter surfaceDeleter,
            IGeometryAdder surfaceAdder,
            IDocumentServer documentServer)
        {
            m_planeCreator = planeCreator;
            m_surfaceDeleter = surfaceDeleter;
            m_surfaceAdder = surfaceAdder;
            m_documentServer = documentServer;
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command)
        {
            return HandleCommand(commandExecuter, (CommandCreatePlane)command);
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, CommandCreatePlane command)
        {
            var document = m_documentServer.Document;

            m_surfaceDeleter.Delete(command.Name);

            var plane = m_planeCreator();
            plane.Set(command.Center, command.Normal, command.Name);
            plane.Length = command.Length;
            plane.Width = command.Width;

            m_surfaceAdder.Add(plane, document);

            return new Response { Success = true };
        }
    }
}
