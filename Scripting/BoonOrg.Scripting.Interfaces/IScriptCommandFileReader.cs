// (c) 2017 Roland Boon

using BoonOrg.Commands;
using BoonOrg.Storage;

namespace BoonOrg.Scripting
{
    public interface IScriptCommandFileReader : IMessageHandler
    {
        event ResultMessageEventHandler ResultMessageHandler;

        event ErrorMessageEventHandler ErrorMessageHandler;

        IScriptCommandFile ScriptCommandFile { get; }

        void Read(IDocument document, string filePath);
    }
}
