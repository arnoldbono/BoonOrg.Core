// (c) 2019, 2024 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Registrations
{
    public interface ICompositionRootBuilder
    {
        IReadOnlyList<IModuleRegistration> LoadModuleRegistrations();

        IResolver Build(string selectedApplication);

        IResolver Build(string selectedApplication, string[] moduleNames);
    }
}
