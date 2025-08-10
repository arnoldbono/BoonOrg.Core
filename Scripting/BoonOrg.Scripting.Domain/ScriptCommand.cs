// (c) 2017 Roland Boon

using System.Collections.Generic;
using System.Linq;

using BoonOrg.Functions;
using BoonOrg.Commands;
using BoonOrg.Identification;
using BoonOrg.Storage;

namespace BoonOrg.Scripting.Domain
{
    internal sealed class ScriptCommand : IScriptCommand
    {
        private readonly List<IFunctionCall> m_functionCalls = new();

        public string Key { get; }

        public string Text { get; }

        public ScriptCommand(string key, string parameters)
        {
            Key = key;
            Text = parameters;
        }

        public void AddFunctionCall(IFunctionCall functionCall)
        {
            m_functionCalls.Add(functionCall);
        }

        public bool Execute(ICommandExecuter commandExecuter, IDocument document)
        {
            var container = document.Get<IIdentifiableContainer>();
            if (!m_functionCalls.Any())
            {
                return false;
            }

            foreach (IFunctionCall functionCall in m_functionCalls)
            {
                if (functionCall is IExecuteWithCommandExecuter executer)
                {
                    if (!executer.Execute(commandExecuter))
                    {
                        return false;
                    }
                    continue;
                }

                if (!functionCall.Execute(container))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return Key + @" : " + Text;
        }
    }
}
