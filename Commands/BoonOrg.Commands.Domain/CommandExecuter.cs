// (c) 2017, 2019 Roland Boon

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

using BoonOrg.Commands.Responses;

namespace BoonOrg.Commands.Domain
{
    internal sealed class CommandExecuter : ICommandExecuter, IDisposable
    {
        private readonly ICommandHandlerRegistrar m_registrar;
        private readonly Queue<Tuple<Guid, ICommand, ICommandHandler>> m_commandQueue = new ();
        private readonly Dictionary<Guid, IResponse> m_responses = new ();
        private readonly Mutex m_mutex = new ();

        private Thread m_workerThread;
        private bool m_stop;
        private bool m_stopped;

        public IMessageHandler MessageHandler { get; private set; }

        public CommandExecuter(ICommandHandlerRegistrar registrar)
        {
            m_registrar = registrar;
        }

        public IResponse Execute<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            try
            {
                ICommandHandler commandHandler = m_registrar.GetCommandHandler<TCommand>();
                var response = commandHandler != null ?
                    commandHandler.HandleCommand(this, command) :
                    new Response { Success = false };
                return response;
            }
            catch (Exception ex)
            {
                MessageHandler?.SendError(ex.ToString());
                return new Response { Success = false };
            }
        }

        public TResponse Execute<TCommand, TResponse>(TCommand command)
            where TCommand : class, ICommand
            where TResponse : class, IResponse
        {
            try
            {
                ICommandHandler commandHandler = m_registrar.GetCommandHandler<TCommand>();
                var response = commandHandler != null ?
                    (TResponse)commandHandler.HandleCommand(this, command) :
                    default(TResponse);
                return response;
            }
            catch (Exception ex)
            {
                MessageHandler?.SendError(ex.ToString());
                return default(TResponse);
            }
        }

        public Guid Enqueue<TCommand>(ICommand command)
            where TCommand : class, ICommand
        {
            ICommandHandler commandHandler = m_registrar.GetCommandHandler<TCommand>();
            if (commandHandler == null)
            {
                return Guid.Empty;
            }

            Guid ticket = Guid.NewGuid();

            Lock();
            m_commandQueue.Enqueue(new Tuple<Guid, ICommand, ICommandHandler>(ticket, command, commandHandler));
            Release();

            return ticket;
        }

        public Task<TResponse> ExecuteAsync<TCommand, TResponse>(ICommand command)
            where TCommand : class, ICommand
            where TResponse : class, IResponse
        {
            Lock();
            Guid ticket = Enqueue<TCommand>(command);
            Release();

            return Task.Run(() =>
            {
                var response = default(TResponse);
                while (!m_stop && !TryGetResponse<TResponse>(ticket, out response))
                {
                    // TODO. Use a semaphore to wait.
                    Thread.Sleep(20);
                }
                return response;
            });
        }

        public bool TryGetResponse<TResponse>(Guid ticket, out TResponse response)
            where TResponse : class, IResponse
        {
            Lock();

            bool success = m_responses.TryGetValue(ticket, out IResponse r);
            response = r as TResponse;
            if (success)
            {
                m_responses.Remove(ticket);
            }

            Release();

            return success;
        }

        public void Start(IMessageHandler messageHandler)
        {
            Stop();
            Lock();

            MessageHandler = messageHandler;

            m_workerThread = new Thread(new ThreadStart(WorkerThread)) { Name = nameof(CommandExecuter) };
            m_commandQueue.Clear();
            m_responses.Clear();

            Release();

            m_workerThread.Start();
        }

        public void Stop()
        {
            if (m_workerThread != null)
            {
                m_stop = true;
                while (!m_stopped)
                {
                    Thread.Sleep(20);
                }
            }
            m_workerThread = null;
            m_stop = false;
            m_stopped = false;
        }

        public void Lock()
        {
            m_mutex.WaitOne();
        }

        public void Release()
        {
            m_mutex.ReleaseMutex();
        }

        private bool CommandQueued()
        {
            Lock();
            bool retval = m_commandQueue.Count > 0;
            Release();

            return retval;
        }

        private void GetCommand(out Guid ticket, out ICommand command, out ICommandHandler commandHandler)
        {
            Lock();
            Tuple<Guid, ICommand, ICommandHandler> cmd = m_commandQueue.Dequeue();
            Release();

            ticket = cmd.Item1;
            command = cmd.Item2;
            commandHandler = cmd.Item3;
        }

        private void AddResponse(Guid ticket, IResponse response)
        {
            Lock();
            m_responses.Add(ticket, response);
            Release();
        }

        private void WorkerThread()
        {
            while (!m_stop)
            {
                while (!m_stop && CommandQueued())
                {
                    GetCommand(out Guid ticket, out ICommand command, out ICommandHandler commandHandler);
                    try
                    {
                        IResponse response = commandHandler.HandleCommand(this, command);
                        AddResponse(ticket, response);
                    }
                    catch (Exception ex)
                    {
                        MessageHandler?.SendError(ex.ToString());
                        AddResponse(ticket, new Response { Success = false });
                    }
                }
                if (!m_stop)
                {
                    Thread.Sleep(20);
                }
            }
            m_stopped = true;
        }

        public void Dispose()
        {
            m_mutex.Dispose();
        }
    }
}
