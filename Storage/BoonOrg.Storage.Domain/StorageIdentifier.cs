// (c) 2017, 2018, 2019 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BoonOrg.Identification;
using BoonOrg.Registrations;

namespace BoonOrg.Storage.Domain
{
    internal sealed class StorageIdentifier : IStorageIdentifier
    {
        private readonly int GuidLength = Guid.Empty.ToByteArray().Length;
        private readonly IResolver m_resolver;
        private readonly List<Action<IIdentifiableContainer>> m_postSerializationActions = new();

        public StorageIdentifier(IResolver resolver)
        {
            m_resolver = resolver;
        }

        public string GetTypeIdentifier(Type type)
        {
            if (type == null)
            {
                return null;
            }

            var typeInterfaceName = $@"I{type.Name}";
            var typeInterfaces = type.GetInterfaces();
            var typeInterface = typeInterfaces.FirstOrDefault(t => t.Name.CompareTo(typeInterfaceName) == 0);
            return typeInterface?.ToString() ?? type.ToString();
        }

        public T CreateAndReadIdentification<T>(BinaryReader reader) where T : IIdentifiable
        {
            var identifiable = m_resolver.Resolve<T>();
            identifiable.Identification.Rename(ReadName(reader));
            identifiable.Identification.ResetId(ReadId(reader));
            return identifiable;
        }

        public void ReadIdentification(BinaryReader reader, IIdentifiable identifiable)
        {
            string name = ReadName(reader);
            Guid id = ReadId(reader);
            IIdentity identity = identifiable.Identification;
            identity.Rename(name);
            identity.ResetId(id);
        }

        public string ReadName(BinaryReader reader) => reader.ReadString();

        public Guid ReadId(BinaryReader reader) => new Guid(reader.ReadBytes(GuidLength));

        public void WriteId(BinaryWriter writer, Guid id) => writer.Write(id.ToByteArray());

        public void WriteIdentification<T>(BinaryWriter writer, IIdentifiable identifiable) where T : class
        {
            writer.Write(GetTypeIdentifier(typeof(T)));
            IIdentity identity = identifiable.Identification;
            writer.Write(identity.Name);
            WriteId(writer, identity.Id);
        }

        public void AddPostSerializationAction(Action<IIdentifiableContainer> action)
        {
            m_postSerializationActions.Add(action);
        }

        public void CallPostSerializationActions(IIdentifiableContainer container)
        {
            foreach (var action in m_postSerializationActions)
            {
                action(container);
            }

            m_postSerializationActions.Clear();
        }
    }
}
