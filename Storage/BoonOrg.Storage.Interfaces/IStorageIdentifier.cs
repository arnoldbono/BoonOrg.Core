// (c) 2018 Roland Boon

using System;
using System.IO;

using BoonOrg.Identification;

namespace BoonOrg.Storage
{
    public interface IStorageIdentifier
    {
        string GetTypeIdentifier(Type type);

        T CreateAndReadIdentification<T>(BinaryReader reader) where T : IIdentifiable;

        void ReadIdentification(BinaryReader reader, IIdentifiable identifiable);

        string ReadName(BinaryReader reader);

        Guid ReadId(BinaryReader reader);

        void WriteId(BinaryWriter writer, Guid id);

        void WriteIdentification<T>(BinaryWriter writer, IIdentifiable identifiable) where T : class;

        void AddPostSerializationAction(Action<IIdentifiableContainer> action);

        void CallPostSerializationActions(IIdentifiableContainer container);
    }
}
