// (c) 2017, 2019 Roland Boon

using BoonOrg.Registrations;

using BoonOrg.Scripting.Domain.Operations;
using BoonOrg.Scripting.Operations;

namespace BoonOrg.Scripting.Domain
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterType<ScriptCommandFileReader, IScriptCommandFileReader>();
            registrar.RegisterType<ScriptFunctionParser, IScriptCommandHandler>();
            registrar.RegisterType<ScriptCommandExecutor, IScriptCommandExecutor>();
            registrar.RegisterType<ScriptVariableAdder, IScriptCommandHandler>();

            registrar.RegisterType<OperationCreateVariable, IOperationCreateVariable>();

        }
    }
}
