// (c) 2017, 2019 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Units.Domain
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<UnitSystem, IUnitSystem>();
            registrar.RegisterType<Unit, IUnit>();
            registrar.RegisterType<ValueWithUnit, IValueWithUnit>();
            registrar.RegisterType<ValueWithUnitCreator, IValueWithUnitCreator>();
        }
    }
}
