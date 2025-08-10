// (c) 2017, 2018 Roland Boon

using System;

using BoonOrg.Identification;

namespace BoonOrg.Storage
{
    public interface IStorageRegistration
    {
        void Register<T>() where T : class, IIdentifiable;

        void Register(string typeIdentifier, Func<IStorageProvider> storageProvider);

        string GetTypeIdentifier(IStorageProvider storageProvider);

        IStorageProvider GetStorageProvider(Type type);

        IStorageProvider GetStorageProvider(string typeIdentifier);
    }
}
