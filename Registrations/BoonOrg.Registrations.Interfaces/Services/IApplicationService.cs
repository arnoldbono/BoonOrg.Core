// (c) 2023 Roland Boon

using System;
using System.Reactive;

namespace BoonOrg.Registrations.Services
{
    public interface IApplicationService
    {
        IObservable<Unit> ClosedRequest { get; }

        IObservable<Unit> Closed { get; }

        void CancelClose(string reason);

        bool Close();
    }
}
