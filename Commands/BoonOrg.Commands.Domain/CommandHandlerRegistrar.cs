// (c) 2017 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Registrations;

namespace BoonOrg.Commands.Domain
{
    internal sealed class CommandHandlerRegistrar : ICommandHandlerRegistrar
    {
        private readonly IResolver m_resolver;

        private readonly Dictionary<Type, Type> m_commandHandlerLookup = new Dictionary<Type, Type>();

        public CommandHandlerRegistrar(IResolver resolver)
        {
            m_resolver = resolver;
        }

        public void Register<TCommand, TResponse>()
            where TCommand : ICommand
            where TResponse : IResponse
        {
            Type command = typeof(TCommand);
            m_commandHandlerLookup.Add(command, typeof(ICommandHandler<TCommand, TResponse>));
        }

        public ICommandHandler GetCommandHandler<TCommand>() where TCommand : ICommand
        {
            if (!m_commandHandlerLookup.TryGetValue(typeof(TCommand), out Type commandHandler))
            {
                return null;
            }
            return m_resolver.Resolve(commandHandler) as ICommandHandler;
        }
    }
}
