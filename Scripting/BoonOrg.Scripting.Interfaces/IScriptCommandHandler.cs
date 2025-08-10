// (c) 2017 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Identification;

namespace BoonOrg.Scripting
{
    public interface IScriptCommandHandler
    {
        /// <summary>
        /// One of the Keywords is the first word in a script that triggers this command handler to be used.
        /// </summary>
        string[] Keywords { get; }

        /// <summary>
        /// Parses the string of parameters.
        /// </summary>
        /// <param name="container">Container with global parameters.</param>
        /// <param name="messageHandler">The message handler that receives warnings and errors.</param>
        /// <param name="command">The command for which to add the parameters.</param>
        /// <returns>True iff successful.</returns>
        bool Parse(IIdentifiableContainer container, IMessageHandler messageHandler, IScriptCommand command);
    }
}
