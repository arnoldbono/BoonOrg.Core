// (c) 2017, 2023 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Functions;
using BoonOrg.Storage;

namespace BoonOrg.Scripting
{
    /// <summary>
    /// A script command; syntax: [Key] : [[parameter = value]];
    /// </summary>
    public interface IScriptCommand
    {
        string Key { get; }

        string Text { get; }

        void AddFunctionCall(IFunctionCall functionCall);

        bool Execute(ICommandExecuter commandExecuter, IDocument document);
    }
}
