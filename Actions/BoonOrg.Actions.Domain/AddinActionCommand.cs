// (c) 2019 Roland Boon

using System;
using BoonOrg.Commands;

namespace BoonOrg.Actions
{
    internal sealed class AddinActionCommand : IAddinActionCommand
    {
        private readonly ICommandExecuter m_commandExecuter;

        public AddinActionCommand(ICommandExecuter commandExecuter)
        {
            m_commandExecuter = commandExecuter;
        }

        public void Execute<T>(Func<T> creator) where T : class, ICommand
        {
            var command = creator();
            m_commandExecuter.Execute(command);
        }
    }
}
