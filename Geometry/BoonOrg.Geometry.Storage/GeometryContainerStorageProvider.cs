// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Storage;
using BoonOrg.Geometry.Logic;

namespace BoonOrg.Geometry.Storage
{
    internal sealed class GeometryContainerStorageProvider : StorageProvider<IGeometryContainer>
    {
        private Dictionary<string, IStorageProvider> m_lookup = new Dictionary<string, IStorageProvider>();

        private readonly IStorageRegistration m_storageRegistration;
        private readonly IGeometryAdder m_geometryAdder;

        public GeometryContainerStorageProvider(Func<IGeometryContainer> func,
            IStorageRegistration storageRegistration,
            IStorageIdentifier storageIdentifier,
            IGeometryAdder geometryAdder) : base(func, storageIdentifier)
        {
            m_storageRegistration = storageRegistration;
            m_geometryAdder = geometryAdder;
        }

        public override void ReadContent(BinaryReader reader, IDocument document, IGeometryContainer geometryContainer)
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

                if (storageProvider.CreateObject(reader, document) is IGeometry geometry)
                {
                    storageProvider.ReadObjectContent(reader, document, geometry);
                    m_geometryAdder.Add(geometry, document);
                }
            }
        }

        public override void WriteContent(IGeometryContainer geometryContainer, BinaryWriter writer)
        {
            var geometries = geometryContainer.ToList();
            var storeableGeometryTypes = geometries.GroupBy(i => i.GetType())
                .Select(g => g.Key)
                .Where(t => GetStorageProvider(t) != null).ToList();
            var storeableGeometries = geometries.Where(i => storeableGeometryTypes.Contains(i.GetType())).ToList();
            int count = storeableGeometries.Count();
            writer.Write(count);

            foreach (IGeometry geometry in storeableGeometries)
            {
                IStorageProvider storageProvider = GetStorageProvider(geometry.GetType());
                storageProvider.WriteObject(geometry, writer);
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
