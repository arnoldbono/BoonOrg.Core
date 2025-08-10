// (c) 2017, 2019, 2024 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Scripting.Addin
{
    public class ModuleRegistration : IModuleRegistration
    {
        public string Application => string.Empty;

        public string Module => @"BoonOrg.Scripting";

        public string[] ModuleDependencies => System.Array.Empty<string>();

        public void Register(ITypeRegistrar registrar)
        {
            Domain.ModuleRegistration.Register(registrar);
        }

        public void PostRegistration(IResolver resolver)
        {
        }
    }
}
