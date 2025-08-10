// (c) 2017, 2018, 2019, 2023 Roland Boon

using System;

using BoonOrg.Actions;
using BoonOrg.Commands;
using BoonOrg.Functions;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Registrations;
using BoonOrg.Storage;

namespace BoonOrg.Geometry.Addin
{
    public class ModuleRegistration : IModuleRegistration
    {
        public string Application => string.Empty;

        public string Module => @"BoonOrg.Geometry";

        public string[] ModuleDependencies => Array.Empty<string>();

        public void Register(ITypeRegistrar registrar)
        {
            Domain.ModuleRegistration.Register(registrar);
            Storage.ModuleRegistration.Register(registrar);
            Logic.ModuleRegistration.Register(registrar);
        }

        public void PostRegistration(IResolver resolver)
        {
            var storageRegistration = resolver.Resolve<IStorageRegistration>();
            Storage.ModuleRegistration.StorageRegistration(storageRegistration);

            var functionCallRegistrar = resolver.Resolve<IFunctionCallRegistrar>();
            Logic.ModuleRegistration.FunctionRegistration(functionCallRegistrar);

            var commandRegistrar = resolver.Resolve<ICommandHandlerRegistrar>();
            Logic.ModuleRegistration.CommandHandlerRegistration(commandRegistrar);

            var menuItemRegistrar = resolver.Resolve<IAddinMenuItemRegistrar>();
            Logic.ModuleRegistration.MenuItemRegistration(menuItemRegistrar);

            var attributeTypeRegistrations = resolver.Resolve<IAttributeTypeRegistrations>();
            attributeTypeRegistrations.Register(typeof(IOpacityAttribute));
            attributeTypeRegistrations.Register(typeof(IDrawSurfaceAttribute));
            attributeTypeRegistrations.Register(typeof(ISolidMaterialAttribute));
            attributeTypeRegistrations.Register(typeof(ITextureMapAttribute));
        }
    }
}
