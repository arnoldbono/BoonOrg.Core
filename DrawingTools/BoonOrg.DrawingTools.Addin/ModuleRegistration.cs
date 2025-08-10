// (c) 2023 Roland Boon

using System;

using BoonOrg.Registrations;

namespace BoonOrg.DrawingTools.Addin
{
    public class ModuleRegistration : IModuleRegistration
    {
        public string Application => string.Empty;

        public string Module => @"BoonOrg.DrawingTools";

        public string[] ModuleDependencies => Array.Empty<string>();

        public void Register(ITypeRegistrar registrar)
        {
            Domain.ModuleRegistration.Register(registrar);
            Logic.ModuleRegistration.Register(registrar);
        }

        public void PostRegistration(IResolver resolver)
        {
        }
    }
}
