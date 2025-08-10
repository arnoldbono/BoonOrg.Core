// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Commands;
using BoonOrg.Identification;
using BoonOrg.Storage;

namespace BoonOrg.Scripting.Domain
{
    internal sealed class ScriptCommandExecutor : IScriptCommandExecutor
    {
        private readonly Dictionary<string, IScriptCommandHandler> m_commandHandlers = new();
        private readonly IDocumentServer m_documentServer;

        public ScriptCommandExecutor(IEnumerable<IScriptCommandHandler> scriptCommandHandlers, IDocumentServer documentServer)
        {
            m_documentServer = documentServer;

            foreach (IScriptCommandHandler scriptCommandHandler in scriptCommandHandlers)
            {
                foreach (string keyword in scriptCommandHandler.Keywords)
                {
                    RegisterHandler(keyword, scriptCommandHandler);
                }
            }
        }

        public void Execute(ICommandExecuter commandExecuter, IScriptCommandFile scriptCommandFile, IMessageHandler messageHandler)
        {
            var document = m_documentServer.Document;

            var container = document.GetOrCreate<IIdentifiableContainer>();
            container.Clear();

            try
            {
                commandExecuter.Start(messageHandler);

                var commands = new List<IScriptCommand>();
                var loops = new List<ScriptCommandLoop>();
                var loopDepth = 0;

                foreach (var command in scriptCommandFile.Commands)
                {
                    var key = command.Key.ToLower();
                    if (key == @"loop")
                    {
                        var loop = new ScriptCommandLoop();
                        loops.Add(loop);
                        commands.Add(loop);

                        ++loopDepth;
                        continue;
                    }

                    if (key == @"end")
                    {
                        if (loopDepth == 0)
                        {
                            messageHandler.SendError(@"end-loop mismatch");
                            return;
                        }

                        --loopDepth;
                        continue;
                    }

                    if (!m_commandHandlers.TryGetValue(key, out IScriptCommandHandler commandHandler))
                    {
                        messageHandler.SendError($@"Missing command handler for ""{command}""");
                        return;
                    }

                    if (!commandHandler.Parse(container, messageHandler, command))
                    {
                        messageHandler.SendError($@"Failed to parse command ""{command}""");
                        return;
                    }

                    if (loopDepth > 0)
                    {
                        loops[loopDepth - 1].AddCommand(command);
                    }
                    else
                    {
                        commands.Add(command);
                    }
                }

                foreach (var command in commands)
                {
                    if (!command.Execute(commandExecuter, document))
                    {
                        messageHandler.SendError($@"Failed to execute command ""{command}""");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                messageHandler.SendError(ex.Message);
            }
            finally
            {
                commandExecuter.Stop();
            }

            container.Clear();
        }

        private void RegisterHandler(string command, IScriptCommandHandler scriptCommandHandler)
        {
            m_commandHandlers[command.ToLower()] = scriptCommandHandler;
        }
    }
}
