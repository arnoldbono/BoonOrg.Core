// (c) 2017, 2018 Roland Boon

using System;
using System.IO;

using BoonOrg.Identification;

namespace BoonOrg.Storage
{
    public interface IStorageProvider<T> : IStorageProvider where T : class, IIdentifiable
    {
        T Create(BinaryReader reader, IDocument document);

        void ReadContent(BinaryReader reader, IDocument document, T @object);

        void Write(T @object, BinaryWriter writer);

        void WriteContent(T @object, BinaryWriter writer);
    }

    public interface IStorageProvider
    {
        IIdentifiable CreateObject(BinaryReader reader, IDocument document);

        void ReadObjectContent(BinaryReader reader, IDocument document, IIdentifiable @object);

        void WriteObject(IIdentifiable @object, BinaryWriter writer);

        void WriteObjectContent(IIdentifiable @object, BinaryWriter writer);

        bool SupportsType(Type type);
    }
}
