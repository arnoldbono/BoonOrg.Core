// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Registrations
{
    public interface IResolver
    {
        T Resolve<T>();

        object Resolve(Type serviceType);

        bool TryResolve<T>(out T instance) where T : class;

        bool TryResolve(Type serviceType, out object instance);
    }
}
