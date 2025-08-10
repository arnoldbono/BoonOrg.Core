// (c) 2017, 2018 Roland Boon

namespace BoonOrg.Commands
{
    public interface ICommandHandler
    {
        IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command);
    }

    public interface ICommandHandler<TCommand, TResponse> : ICommandHandler where TCommand : ICommand where TResponse : IResponse
    {
        TResponse HandleCommand(ICommandExecuter commandExecuter, TCommand command);
    }
}
