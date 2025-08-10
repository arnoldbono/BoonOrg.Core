// (c) 2017, 2019 Roland Boon

using BoonOrg.Registrations;
using BoonOrg.Functions.Creators;
using BoonOrg.Functions.Domain.Creators;

namespace BoonOrg.Functions.Domain
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<FunctionCallRegistrar, IFunctionCallRegistrar>();

            registrar.RegisterType<ParameterCollection, IParameterCollection>();
            registrar.RegisterType<Parameter, IParameter>();
            registrar.RegisterType<ParameterCreator, IParameterCreator>();
            registrar.RegisterType<ParameterParser, IParameterParser>();
        }
    }
}
