// (c) 2023 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Storage.Logic
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterType<IdentifiableStreamWriterService, IIdentifiableStreamWriterService>();
        }
    }
}
