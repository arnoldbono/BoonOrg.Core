// (c) 2018, 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Identification;

namespace BoonOrg.Storage
{
    public abstract class StorageProvider<T> : IStorageProvider<T> where T : class, IIdentifiable
    {
        private readonly Func<T> m_func;

        protected IStorageIdentifier StorageIdentifier { get; }

        public StorageProvider(Func<T> func, IStorageIdentifier storageIdentifier)
        {
            m_func = func;
            StorageIdentifier = storageIdentifier;
        }

        public void ReadObject(BinaryReader reader, IDocument document, IIdentifiable @object)
        {
            ReadObjectContent(reader, document, @object);
        }

        public void WriteObject(IIdentifiable @object, BinaryWriter writer)
        {
            var identifiable = (T)@object;
            Write(identifiable, writer);
        }

        public IIdentifiable CreateObject(BinaryReader reader, IDocument document)
        {
            var @object = m_func();
            StorageIdentifier.ReadIdentification(reader, @object);
            return @object;
        }

        public void WriteObjectContent(IIdentifiable @object, BinaryWriter writer)
        {
            WriteContent((T)@object, writer);
        }

        public virtual T Create(BinaryReader reader, IDocument document)
        {
            var @object = m_func();
            StorageIdentifier.ReadIdentification(reader, @object);
            return @object;
        }

        public virtual void Write(T @object, BinaryWriter writer)
        {
            StorageIdentifier.WriteIdentification<T>(writer, @object);
            WriteContent(@object, writer);
        }

        public void ReadObjectContent(BinaryReader reader, IDocument document, IIdentifiable @object) =>
            ReadContent(reader, document, (T)@object);

        public abstract void ReadContent(BinaryReader reader, IDocument document, T @object);

        public abstract void WriteContent(T @object, BinaryWriter writer);

        public bool SupportsType(Type type) => typeof(T).IsAssignableFrom(type);
    }
}
