// (c) 2017, 2019 Roland Boon

using BoonOrg.Registrations;

using BoonOrg.Commands.Domain;

namespace BoonOrg.Commands.Addin
{
    public class ModuleRegistration : IModuleRegistration
    {
        public string Application => string.Empty;

        public string Module => @"BoonOrg.Commands";

        public string[] ModuleDependencies => new string[] { };

        public void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<CommandHandlerRegistrar, ICommandHandlerRegistrar>();
            registrar.RegisterTypeAsSingleton<CommandExecuter, ICommandExecuter>();
        }

        public void PostRegistration(IResolver resolver)
        {
        }
    }
}
