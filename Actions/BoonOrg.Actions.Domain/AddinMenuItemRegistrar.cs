// (c) 2017 Roland Boon

using System;
using System.Collections.Generic;
using BoonOrg.Commands;
using BoonOrg.Registrations;

namespace BoonOrg.Actions.Domain
{
    internal sealed class AddinMenuItemRegistrar : IAddinMenuItemRegistrar
    {
        private readonly IResolver m_resolver;
        private readonly Dictionary<string, IAddinMenuItem> m_menuItems = new Dictionary<string, IAddinMenuItem>();

        public AddinMenuItemRegistrar(IResolver resolver)
        {
            m_resolver = resolver;
        }

        public void Add<T>() where T : IAddinMenuItem
        {
            Add(m_resolver.Resolve<T>());
        }

        public void Add(string path, Action action)
        {
            Add(new AddinMenuItem(path, action));
        }

        public void Add<T>(string path) where T : class, IAddinCommand
        {
            Add(new AddinMenuItem(path, () =>
            {
                var dlg = m_resolver.Resolve<T>();
                dlg.Execute();
            }));
        }

        public void AddAction<T>() where T : IAddinAction
        {
            Add(m_resolver.Resolve<T>().MenuItem);
        }

        public void AddActionCommand<T>(string path) where T : class, ICommand
        {
            var commandAction = m_resolver.Resolve<IAddinActionCommand>();
            var command = m_resolver.Resolve<Func<T>>();

            Add(path, () => commandAction.Execute(command));
        }

        public void Add(IAddinMenuItem menuItem)
        {
            m_menuItems.Add(menuItem.Path, menuItem);
        }

        public IEnumerable<IAddinMenuItem> GetAll()
        {
            return m_menuItems.Values;
        }
    }
}
