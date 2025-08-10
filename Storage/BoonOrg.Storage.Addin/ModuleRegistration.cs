// (c) 2017, 2018, 2019, 2023 Roland Boon

using BoonOrg.Registrations;
using BoonOrg.Functions;

namespace BoonOrg.Storage.Addin
{
    public class ModuleRegistration : IModuleRegistration
    {
        public string Application => string.Empty;

        public string Module => @"BoonOrg.Storage";

        public string[] ModuleDependencies => new string[] { };

        public void Register(ITypeRegistrar registrar)
        {
            Domain.ModuleRegistration.Register(registrar);
            Logic.ModuleRegistration.Register(registrar);
        }

        public void PostRegistration(IResolver resolver)
        {
            var registrar = resolver.Resolve<IFunctionCallRegistrar>();
            Domain.ModuleRegistration.FunctionRegistration(registrar);

            var storageRegistration = resolver.Resolve<IStorageRegistration>();
            Domain.ModuleRegistration.StorageRegistration(storageRegistration);
        }
    }
}
