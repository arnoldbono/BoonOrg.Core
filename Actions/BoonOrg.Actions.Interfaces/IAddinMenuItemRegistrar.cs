// (c) 2017, 2019 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Commands;

namespace BoonOrg.Actions
{
    public interface IAddinMenuItemRegistrar
    {
        void Add<T>() where T : IAddinMenuItem;

        void Add(string path, Action action);

        void Add<T>(string path) where T : class, IAddinCommand;

        void AddActionCommand<T>(string path) where T : class, ICommand;

        void Add(IAddinMenuItem menuItem);

        void AddAction<T>() where T : IAddinAction;

        IEnumerable<IAddinMenuItem> GetAll();
    }
}
