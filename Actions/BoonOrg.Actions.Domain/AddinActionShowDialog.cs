// (c) 2017, 2019 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Registrations;

namespace BoonOrg.Actions
{
    public class AddinActionShowDialog<T> : IAddinCommand where T : class, ICommand
    {
        private readonly ICommandExecuter m_commandExecuter;
        private readonly IResolver m_resolver;

        public AddinActionShowDialog(ICommandExecuter commandExecuter, IResolver resolver)
        {
            m_commandExecuter = commandExecuter;
            m_resolver = resolver;
        }

        public void Execute()
        {
            var command = m_resolver.Resolve<T>();
            m_commandExecuter.Execute(command);
        }
    }
}
