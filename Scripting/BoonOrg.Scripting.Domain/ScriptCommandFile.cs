// (c) 2017 Roland Boon

using System.Collections.Generic;
using System.Linq;

namespace BoonOrg.Scripting.Domain
{
    internal sealed class ScriptCommandFile : IScriptCommandFile
    {
        private readonly List<string> m_lines;
        private readonly List<IScriptCommand> m_commands;

        public IEnumerable<string> Lines { get { return m_lines; } }

        public IEnumerable<IScriptCommand> Commands { get { return m_commands; } }

        public ScriptCommandFile(IEnumerable<string> lines, IEnumerable<IScriptCommand> commands)
        {
            m_lines = lines.ToList();
            m_commands = commands.ToList();
        }
    }
}
