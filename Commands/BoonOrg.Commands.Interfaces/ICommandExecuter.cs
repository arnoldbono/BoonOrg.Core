// (c) 2017 Roland Boon

using System;
using System.Threading.Tasks;

namespace BoonOrg.Commands
{
    public interface ICommandExecuter
    {
        IMessageHandler MessageHandler { get; }

        Guid Enqueue<TCommand>(ICommand command) where TCommand : class, ICommand;

        IResponse Execute<TCommand>(TCommand command)
            where TCommand : class, ICommand;

        TResponse Execute<TCommand, TResponse>(TCommand command)
            where TCommand : class, ICommand
            where TResponse : class, IResponse;

        Task<TResponse> ExecuteAsync<TCommand, TResponse>(ICommand command)
            where TCommand : class, ICommand
            where TResponse : class, IResponse;

        bool TryGetResponse<TResponse>(Guid ticket, out TResponse response)
            where TResponse : class, IResponse;
        
        void Start(IMessageHandler messageHandler);

        void Stop();
    }
}