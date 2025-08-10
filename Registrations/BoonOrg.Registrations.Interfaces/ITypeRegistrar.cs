// (c) 2017 Roland Boon

namespace BoonOrg.Registrations
{
    public interface ITypeRegistrar
    {
        void RegisterType<TConcrete, TInterface>() where TConcrete : class, TInterface;

        void RegisterType<T>() where T : class;

        void RegisterTypeAsSingleton<TConcrete, TInterface>() where TConcrete : class, TInterface;
    }
}
