// (c) 2018, 2023 Roland Boon

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Storage.Domain
{
    internal sealed class IdentifiableContainerStorageProvider : StorageProvider<IIdentifiableContainer>
    {
        private readonly IStorageRegistration m_storageRegistration;
        private Dictionary<string, IStorageProvider> m_lookup = new Dictionary<string, IStorageProvider>();

        public IdentifiableContainerStorageProvider(Func<IIdentifiableContainer> func,
            IStorageRegistration storageRegistration,
            IStorageIdentifier storageIdentifier) : base(func, storageIdentifier)
        {
            m_storageRegistration = storageRegistration;
        }

        public override void ReadContent(BinaryReader reader, IDocument document, IIdentifiableContainer container)
        {
            int count = reader.ReadInt32();

            for (int i = 0; i < count; ++i)
            {
                string typeIdentifier = reader.ReadString();
                IStorageProvider storageProvider = GetStorageProvider(typeIdentifier);
                if (storageProvider == null)
                {
                    break;
                }

                var @object = storageProvider.CreateObject(reader, document);
                if (@object is IIdentifiable item)
                {
                    storageProvider.ReadObjectContent(reader, document, item);
                    container.Add(item);
                }
            }
        }

        public override void WriteContent(IIdentifiableContainer container, BinaryWriter writer)
        {
            var items = container.ToList();
            var storeableItemTypes = items.GroupBy(i => i.GetType())
                .Select(g => g.Key)
                .Where(t => GetStorageProvider(t) != null).ToList();
            var storeableItems = items.Where(i => storeableItemTypes.Contains(i.GetType())).ToList();
            int count = storeableItems.Count();
            writer.Write(count);

            foreach (IIdentifiable item in storeableItems)
            {
                IStorageProvider storageProvider = GetStorageProvider(item.GetType());
                storageProvider.WriteObject(item, writer);
            }
        }

        private IStorageProvider GetStorageProvider(string typeIdentifier)
        {
            if (m_lookup.ContainsKey(typeIdentifier))
            {
                return m_lookup[typeIdentifier];
            }

            var storageProvider = m_storageRegistration.GetStorageProvider(typeIdentifier);
            if (storageProvider != null)
            {
                m_lookup.Add(typeIdentifier, storageProvider);
            }
            return storageProvider;
        }

        private IStorageProvider GetStorageProvider(Type type)
        {
            var typeIdentifier = StorageIdentifier.GetTypeIdentifier(type);

            var storageProvider = GetStorageProvider(typeIdentifier);
            if (storageProvider != null)
            {
                return storageProvider;
            }

            storageProvider = m_storageRegistration.GetStorageProvider(type); // Deeper search
            if (storageProvider != null)
            {
                m_lookup.Add(typeIdentifier, storageProvider);
            }

            return storageProvider;
        }
    }
}
