// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Storage;
using BoonOrg.Registrations;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Storage.Attributes
{
    internal sealed class GeometryAttributeContainerStorageProvider : StorageProvider<IGeometryAttributeContainer>
    {
        private readonly IResolver m_resolver;
        private readonly IAttributeTypeRegistrations m_attributeTypeRegistrations;

        public GeometryAttributeContainerStorageProvider(Func<IGeometryAttributeContainer> geometryAttributeContainerFunc,
            IResolver resolver,
            IAttributeTypeRegistrations attributeTypeRegistrations,
            IStorageIdentifier storageIdentifier) : base(geometryAttributeContainerFunc, storageIdentifier)
        {
            m_resolver = resolver;
            m_attributeTypeRegistrations = attributeTypeRegistrations;
        }

        public override void ReadContent(BinaryReader reader, IDocument document, IGeometryAttributeContainer geometryAttributeContainer)
        {
            var geometryContainer = document.Get<IGeometryContainer>();

            int count = reader.ReadInt32();

            var attributeTypes = new Dictionary<int, string>();
            for (int i = 0; i < count; ++i)
            {
                attributeTypes.Add(reader.ReadInt32(), reader.ReadString());
            }

            count = reader.ReadInt32();

            var byteLengthGuid = Guid.Empty.ToByteArray().Length;

            for (int i = 0; i < count; ++i)
            {
                var id = new Guid(reader.ReadBytes(byteLengthGuid));
                var geometry = geometryContainer.Get(id);
                if (geometry == null)
                {
                    return;
                }

                var attributeCount = reader.ReadInt32();

                for (var j = 0; j < attributeCount; ++j)
                {
                    var attributeTypeId = reader.ReadInt32();
                    var attributeTypeName = attributeTypes[attributeTypeId];

                    var registratedType = Type.GetType(attributeTypeName);
                    var geometryAttributeStorageProviderType = typeof(IGeometryAttributeStorageProvider<>).MakeGenericType(registratedType);
                    var storageProvider = (IGeometryAttributeStorageProvider)m_resolver.Resolve(geometryAttributeStorageProviderType);

                    var attribute = m_attributeTypeRegistrations.Create(attributeTypeName);
                    storageProvider.Read(reader, attribute);

                    geometryAttributeContainer.AddAttributeOfType(geometry, attribute, registratedType);
                }
            }
        }

        public override void WriteContent(IGeometryAttributeContainer geometryAttributeContainer, BinaryWriter writer)
        {
            var registrations = m_attributeTypeRegistrations.Registrations.ToArray();
            writer.Write(registrations.Length);

            foreach (var registration in registrations)
            {
                writer.Write(registration.TypeId);
                writer.Write(registration.TypeName);
            }

            var itemIds = geometryAttributeContainer.ItemIds.ToArray();
            writer.Write(itemIds.Length);

            foreach (var itemId in itemIds)
            {
                writer.Write(itemId.ToByteArray());

                var attributes = geometryAttributeContainer.GetAttributes(itemId).ToArray();
                writer.Write(attributes.Length);

                foreach (var attribute in attributes)
                {
                    var registration = m_attributeTypeRegistrations.GetRegistration(attribute);
                    writer.Write(registration.TypeId);

                    var registratedType = Type.GetType(registration.TypeName);
                    var geometryAttributeStorageProviderType = typeof(IGeometryAttributeStorageProvider<>).MakeGenericType(registratedType);
                    var storageProvider = (IGeometryAttributeStorageProvider)m_resolver.Resolve(geometryAttributeStorageProviderType);

                    storageProvider.Write(writer, attribute);
                }
            }
        }
    }
}
