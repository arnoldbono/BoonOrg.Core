// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Scripting
{
    /// <summary>
    /// A script command file is a text based file that contains script commands.
    /// </summary>
    public interface IScriptCommandFile
    {
        IEnumerable<string> Lines { get; }

        IEnumerable<IScriptCommand> Commands { get; }
    }
}
