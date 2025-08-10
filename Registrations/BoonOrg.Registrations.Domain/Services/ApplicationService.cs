// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;

using BoonOrg.Registrations.Services;

namespace BoonOrg.Registrations
{
    internal sealed class ApplicationService : IApplicationService
    {
        private readonly Subject<Unit> m_closeRequest = new();
        private readonly Subject<Unit> m_closed = new();
        private List<string> m_cancelReasons = new();

        public IObservable<Unit> ClosedRequest => m_closeRequest;

        public IObservable<Unit> Closed => m_closed;


        public void CancelClose(string reason)
        {
            m_cancelReasons.Add(reason);
        }

        public bool Close()
        {
            m_cancelReasons.Clear();
            m_closeRequest.OnNext(Unit.Default);

            if (m_cancelReasons.Any())
            {
                m_cancelReasons.Clear();
                return false;
            }

            m_closed.OnNext(Unit.Default);
            return true;
        }
    }
}
