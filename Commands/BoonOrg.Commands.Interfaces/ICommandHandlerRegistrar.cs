// (c) 2017 Roland Boon

namespace BoonOrg.Commands
{
    public interface ICommandHandlerRegistrar
    {
        void Register<TCommand, TResponse>()
            where TCommand : ICommand
            where TResponse : IResponse;

        ICommandHandler GetCommandHandler<TCommand>() where TCommand : ICommand;
    }
}