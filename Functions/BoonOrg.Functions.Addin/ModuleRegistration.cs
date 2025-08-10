// (c) 2017, 2019 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Functions.Addin
{
    public class ModuleRegistration : IModuleRegistration
    {
        public string Application => string.Empty;

        public string Module => @"BoonOrg.Functions";

        public string[] ModuleDependencies => new string[] { };

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
