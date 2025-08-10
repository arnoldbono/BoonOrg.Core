// (c) 2017, 2019 Roland Boon

using BoonOrg.Identification.Domain;
using BoonOrg.Registrations;

namespace BoonOrg.Identification.Addin
{
    public class ModuleRegistration : IModuleRegistration
    {
        public string Application => string.Empty;

        public string Module => @"BoonOrg.Registrations";

        public string[] ModuleDependencies => new string[] { };

        public void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterType<IdentifiableContainer, IIdentifiableContainer>();
            registrar.RegisterType<Identity, IIdentity>();
        }

        public void PostRegistration(IResolver resolver)
        {

        }
    }
}
