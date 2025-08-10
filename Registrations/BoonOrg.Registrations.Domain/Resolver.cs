// (c) 2017, 2019, 2024 Roland Boon

using System;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

using Autofac;

using BoonOrg.Registrations.Services;

namespace BoonOrg.Registrations.Domain
{
    public sealed class Resolver : IResolver, ITypeRegistrar, ICompositionRootBuilder, IDisposable
    {
        private static Resolver m_instance;

        private readonly ContainerBuilder m_builder = new ContainerBuilder();

        private ILifetimeScope m_scope;
        private IReadOnlyList<IModuleRegistration> m_moduleRegistrations;

        private Resolver()
        {
            m_builder.RegisterInstance<IResolver>(this);
            RegisterTypeAsSingleton<ApplicationService, IApplicationService>();
        }

        public static IResolver Build()
        {
            var builder = Builder();
            return builder.Build(@"BFCS");
        }

        public IResolver Build(string selectedApplication)
        {
            return Build(selectedApplication, null);
        }

        public IResolver Build(string selectedApplication, string[] moduleNames)
        {
            if (moduleNames ==  null)
            {
                moduleNames = Array.Empty<string>();
            }

            if (m_moduleRegistrations == null)
            {
                LoadModuleRegistrations();
            }

            var selectedRegistrators =
                m_moduleRegistrations.Where(r => string.IsNullOrEmpty(r.Application) ||
                    string.Compare(r.Application, selectedApplication, true) == 0 ||
                    moduleNames.Any(x => string.Compare(r.Module, x, true) == 0)).ToList();

            AddDependentModules(selectedRegistrators, selectedRegistrators.Select(x => x.Module).ToList());

            Build(selectedRegistrators);

            return this;
        }

        private void AddDependentModules(List<IModuleRegistration> selectedRegistrators, IReadOnlyCollection<string> modules)
        {
            while (true)
            {
                var modulesAdded = new List<string>();

                foreach (var module in modules)
                {
                    var registrar = selectedRegistrators.First(x => x.Module == module);
                    foreach (var dependency in registrar.ModuleDependencies)
                    {
                        if (!selectedRegistrators.Any(x => string.Compare(x.Module, dependency, true) == 0))
                        {
                            var moduleRegistration = m_moduleRegistrations.FirstOrDefault(x => string.Compare(x.Module, dependency, true) == 0);
                            if (moduleRegistration != null)
                            {
                                selectedRegistrators.Add(moduleRegistration);
                                modulesAdded.Add(moduleRegistration.Module);
                            }
                        }
                    }
                }

                if (!modulesAdded.Any())
                {
                    break;
                }

                modules = modulesAdded;
            }
        }

        public IReadOnlyList<IModuleRegistration> LoadModuleRegistrations()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            string path = Path.GetDirectoryName(assembly.Location);

            // Find DLLs with the correct extension and add these dynamically.
            var registrators = new List<IModuleRegistration>();
            foreach (var fileName in Directory.EnumerateFiles(path, @"*.Addin.dll"))
            {
                IModuleRegistration registrator = GetAddinModuleRegistration(fileName);
                if (registrator != null)
                {
                    registrators.Add(registrator);
                }
            }

            m_moduleRegistrations = registrators;
            return registrators;
        }

        private void Build(IReadOnlyList<IModuleRegistration> registrators)
        {
            foreach (IModuleRegistration registrator in registrators)
            {
                registrator.Register(this);
            }

            IContainer container = m_builder.Build();

            m_scope = container.BeginLifetimeScope();

            foreach (IModuleRegistration registrator in registrators)
            {
                registrator.PostRegistration(this);
            }
        }

        private static IModuleRegistration GetAddinModuleRegistration(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }

            Assembly assembly = Assembly.LoadFrom(fileName);
            Type registratorType = assembly.GetTypes().FirstOrDefault(t => t.Name == @"ModuleRegistration");
            if (registratorType != null)
            {
                return Activator.CreateInstance(registratorType) as IModuleRegistration;
            }

            return null;
        }

        public static IResolver Instance()
        {
            if (m_instance == null)
            {
                m_instance = new Resolver();
            }
            return m_instance;
        }

        public static ICompositionRootBuilder Builder()
        {
            return (ICompositionRootBuilder)Instance();
        }

        public T Resolve<T>()
        {
            return m_scope.Resolve<T>();
        }

        public bool TryResolve<T>(out T instance) where T : class
        {
            return m_scope.TryResolve(out instance);
        }

        public object Resolve(Type serviceType)
        {
            return m_scope.Resolve(serviceType);
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return m_scope.TryResolve(serviceType, out instance);
        }

        public void Dispose()
        {
            if (m_scope != null)
            {
                m_scope.Dispose();
                m_scope = null;
            }
            GC.SuppressFinalize(this);
        }

        public void RegisterType<TConcrete, TInterface>() where TConcrete : class, TInterface
        {
            m_builder.RegisterType<TConcrete>().As<TInterface>();
        }

        public void RegisterType<T>() where T : class
        {
            m_builder.RegisterType<T>().As<T>();
        }

        public void RegisterTypeAsSingleton<TConcrete, TInterface>() where TConcrete : class, TInterface
        {
            m_builder.RegisterType<TConcrete>().As<TInterface>().SingleInstance();
        }

        // protected virtual void Dispose(bool disposing) is not needed due to sealed
    }
}
