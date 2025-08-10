// (c) 2017, 2018 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Identification;
using BoonOrg.Registrations;

namespace BoonOrg.Storage.Domain
{
    internal sealed class StorageRegistration : IStorageRegistration
    {
        private readonly Dictionary<string, Func<IStorageProvider>> m_registrations =
            new Dictionary<string, Func<IStorageProvider>>();
        private readonly Dictionary<string, IStorageProvider> m_cache =
            new Dictionary<string, IStorageProvider>();

        private readonly IStorageIdentifier m_typeIdentifier;
        private readonly IResolver m_resolver;

        public StorageRegistration(IStorageIdentifier typeIdentifier, IResolver resolver)
        {
            m_typeIdentifier = typeIdentifier;
            m_resolver = resolver;
        }

        private string GetTypeIdentifier(Type type)
        {
            return m_typeIdentifier.GetTypeIdentifier(type);
        }

        public void Register<T>() where T : class, IIdentifiable
        {
            IStorageProvider<T> StorageProvider() => m_resolver.Resolve<IStorageProvider<T>>();
            m_registrations.Add(GetTypeIdentifier(typeof(T)), StorageProvider);
        }

        public void Register(string typeIdentifier, Func<IStorageProvider> storageProvider)
        {
            m_registrations.Add(typeIdentifier, storageProvider);
        }

        public IStorageProvider GetStorageProvider(Type type)
        {
            if (type == null)
            {
                return null;
            }

            var typeIdentifier = GetTypeIdentifier(type);
            IStorageProvider storageProvider = GetStorageProvider(typeIdentifier);
            if (storageProvider != null)
            {
                return storageProvider;
            }

            foreach (string typeIdentifierKey in m_registrations.Keys)
            {
                storageProvider = GetStorageProvider(typeIdentifierKey);
                if (storageProvider.SupportsType(type))
                {
                    m_cache.Add(typeIdentifier, storageProvider);
                    return storageProvider;
                }
            }

            return null;
        }

        public IStorageProvider GetStorageProvider(string typeIdentifier)
        {
            if (m_cache.ContainsKey(typeIdentifier))
            {
                return m_cache[typeIdentifier];
            }

            if (!m_registrations.ContainsKey(typeIdentifier))
            {
                return null;
            }

            var storageProvider = m_registrations[typeIdentifier]();
            m_cache.Add(typeIdentifier, storageProvider);

            return storageProvider;
        }

        public string GetTypeIdentifier(IStorageProvider storageProvider)
        {
            return m_cache.FirstOrDefault(x => x.Value == storageProvider).Key;
        }
    }
}
