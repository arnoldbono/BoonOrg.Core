// (c) 2017, 2019 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Actions.Domain
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<AddinMenuItemRegistrar, IAddinMenuItemRegistrar>();
            registrar.RegisterType<AddinMenuItem, IAddinMenuItem>();
            registrar.RegisterType<AddinActionCommand, IAddinActionCommand>();
        }
    }
}
