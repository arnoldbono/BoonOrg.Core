// (c) 2019 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Commands.Responses;

using BoonOrg.Geometry.Commands;

namespace BoonOrg.Geometry.Logic.CommandHandlers
{
    internal sealed class CommandHandlerAddGroup : ICommandHandler<CommandAddGroup, IResponse>
    {
        public CommandHandlerAddGroup()
        {
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command)
        {
            return HandleCommand(commandExecuter, (CommandAddGroup)command);
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, CommandAddGroup command)
        {
            // TODO Implement or remove class

            return new Response { Success = true };
        }
    }
}
