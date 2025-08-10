// (c) 2017, 2023 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Identification;

namespace BoonOrg.Scripting
{
    /// <summary>
    /// A script command executor handles commands.
    /// </summary>
    public interface IScriptCommandExecutor
    {
        void Execute(ICommandExecuter commandExecuter, IScriptCommandFile scriptCommandFile, IMessageHandler messageHandler);
    }
}
