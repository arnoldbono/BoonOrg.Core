// (c) 2017, 2019 Roland Boon

namespace BoonOrg.Registrations
{
    public interface IModuleRegistration
    {
        string Application { get; }

        string Module { get; }

        string[] ModuleDependencies { get; }

        void Register(ITypeRegistrar registrar);

        void PostRegistration(IResolver resolver);
    }
}
