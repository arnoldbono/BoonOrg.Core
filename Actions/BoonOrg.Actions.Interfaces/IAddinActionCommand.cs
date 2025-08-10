// (c) 2019 Roland Boon

using System;
using BoonOrg.Commands;

namespace BoonOrg.Actions
{
    public interface IAddinActionCommand
    {
        void Execute<T>(Func<T> creator) where T : class, ICommand;
    }
}
