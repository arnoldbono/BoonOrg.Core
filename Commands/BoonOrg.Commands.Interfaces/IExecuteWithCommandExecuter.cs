// (c) 2017, 2023 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Commands
{
    public interface IExecuteWithCommandExecuter
    {
        bool Execute(ICommandExecuter commandExecuter);
    }
}
