// (c) 2017, 2019 Roland Boon

using BoonOrg.Registrations;

namespace BoonOrg.Functions.Logic
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterType<ParameterTextTryParseDouble, IParameterTextTryParse<double>>();
            registrar.RegisterType<ParameterTextTryParseBool, IParameterTextTryParse<bool>>();
            registrar.RegisterType<ParameterTextTryParseString, IParameterTextTryParse<string>>();
            registrar.RegisterType<ParameterTextTryParseInt, IParameterTextTryParse<int>>();
        }
    }
}
