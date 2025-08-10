// (c) 2017 Roland Boon

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Storage;
using BoonOrg.Commands;

namespace BoonOrg.Scripting.Domain
{
    internal class Message : IMessage
    {
        public string Text { get; }

        public Message(string message)
        {
            Text = message;
        }
    }

    internal sealed class ScriptCommandFileReader : IScriptCommandFileReader
    {
        public event ResultMessageEventHandler ResultMessageHandler;

        public event ErrorMessageEventHandler ErrorMessageHandler;

        public IScriptCommandFile ScriptCommandFile { get; private set; }

        public ScriptCommandFileReader()
        {
        }

        public void Read(IDocument document, string filePath)
        {
            var specialCommands = new List<string> { @"loop", @"end" };
            int commandStartLine = 0;
            int commandStartColumn = 0;
            int commandEndColumn = -1;
            var lines = new List<string>();
            var commands = new List<IScriptCommand>();
            using (var streamReader = File.OpenText(filePath))
            {
                string line = string.Empty;
                bool mustRead = true;
                bool isInsideComment = false;
                while (!string.IsNullOrEmpty(line) || !streamReader.EndOfStream)
                {
                    if (mustRead || string.IsNullOrEmpty(line))
                    {
                        mustRead = false;
                        if (streamReader.EndOfStream)
                        {
                            break;
                        }
                        string text = streamReader.ReadLine();
                        lines.Add(text);
                        if (string.IsNullOrEmpty(line))
                        {
                            commandStartLine++;
                            commandStartColumn = 0;
                            line = text;
                        }
                        else
                        {
                            line += text;
                        }
                    }
                    if (isInsideComment)
                    {
                        int indexComment = line.IndexOf(@"*/");
                        if (indexComment >= 0)
                        {
                            line = line.Substring(indexComment + 2);
                            isInsideComment = false;
                        }
                        else
                        {
                            line = string.Empty;
                            continue;
                        }
                    }
                    if (!isInsideComment)
                    {
                        // TODO. A '#' could be part of a string.
                        int commentIndex = line.IndexOf('#');
                        if (commentIndex >= 0)
                        {
                            line = line.Substring(0, commentIndex);
                        }
                        commentIndex = line.IndexOf(@"//");
                        if (commentIndex >= 0)
                        {
                            line = line.Substring(0, commentIndex);
                        }
                        commentIndex = line.IndexOf(@"/*");
                        if (commentIndex >= 0)
                        {
                            line = line.Substring(0, commentIndex);
                            isInsideComment = true;
                        }
                    }
                    line = line.Trim();
                    if (line.EndsWith(@"\"))
                    {
                        line = line.Substring(0, line.Length - 1); // Don't TrimEnd() !
                        mustRead = true;
                    }
                    if (line.EndsWith(@"\n"))
                    {
                        line = line.Substring(0, line.Length - 2).TrimEnd() + Environment.NewLine;
                        mustRead = true;
                    }
                    if (string.IsNullOrEmpty(line))
                    {
                        mustRead = true;
                        continue;
                    }

                    var command = string.Empty;
                    var commandEnd = line.IndexOf(';');
                    if (commandEnd >= 0)
                    {
                        command = line.Substring(0, commandEnd).Trim();
                        line = line.Substring(commandEnd + 1).TrimStart();
                    }
                    else
                    {
                        foreach (var x in specialCommands)
                        {
                            var match = $@"{x}:";
                            if (line.StartsWith(match, StringComparison.OrdinalIgnoreCase))
                            {
                                command = match;
                                line = string.Empty;
                                break;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(command))
                    {
                        int index = command.IndexOf(':');
                        if (index > 0)
                        {
                            string key = command.Substring(0, index).Trim();
                            string value = command.Substring(index + 1).Trim();

                            int commandEndLine = lines.Count;
                            int startIndex = (commandStartLine == commandEndLine) ? commandStartColumn : 0;
                            string lastLine = lines.Last();
                            commandEndColumn = lastLine.IndexOf(';', startIndex);

                            commands.Add(new ScriptCommand(key, value));
                            commandStartLine = commandEndLine;
                            commandStartColumn = commandEndColumn + 1;
                        }
                        else
                        {
                            SendError(command);
                            break;
                        }
                    }
                    else if (!mustRead)
                    {
                        SendError(line);
                        break;
                    }
                }
            }

            ScriptCommandFile = new ScriptCommandFile(lines, commands);
        }

        public void SendResult(string result)
        {
            ResultMessageHandler?.Invoke(new Message(result));
        }

        public void SendError(string error)
        {
            ErrorMessageHandler?.Invoke(new Message(error));
        }
    }
}
