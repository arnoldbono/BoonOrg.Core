// (c) 2023, 2024 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Functions;
using BoonOrg.Commands;
using BoonOrg.Identification;
using BoonOrg.Storage;

namespace BoonOrg.Scripting.Domain
{
    internal sealed class ScriptCommandLoop : IScriptCommand
    {
        private readonly List<IScriptCommand> m_commands = new();

        public string Key => @"loop";

        public string Text => string.Empty;

        public void AddCommand(IScriptCommand command)
        {
            m_commands.Add(command);
        }

        public void AddFunctionCall(IFunctionCall _)
        {
            // Nothing to do
        }

        public bool Execute(ICommandExecuter commandExecuter, IDocument document)
        {
            var container = document.Get<IIdentifiableContainer>();
            var loopIndex = container.FindByName<IParameter>(@"loopindex");
            var loopEnd = container.FindByName<IParameter>(@"loopend");
            if (loopIndex == null || loopEnd == null)
            {
                return false;
            }

            int index = int.Parse(loopIndex.Text);
            int indexEnd = int.Parse(loopEnd.Text);

            while (index <= indexEnd)
            {
                foreach (var cmd in m_commands)
                {
                    if (!cmd.Execute(commandExecuter, document))
                    {
                        return false;
                    }
                }

                loopIndex.Set((++index).ToString());
            }

            return true;
        }

        public override string ToString()
        {
            return Key;
        }
    }
}
