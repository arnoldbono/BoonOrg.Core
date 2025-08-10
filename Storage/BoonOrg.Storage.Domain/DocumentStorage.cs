// (c) 2017 Roland Boon

using System;
using System.IO;
using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Storage.Domain
{
    internal sealed class DocumentStorage : IDocumentStorage
    {
        private readonly IStorageRegistration m_storageRegistration;
        private readonly IStorageIdentifier m_storageIdentifier;
        private readonly Func<IDocument> m_documentCreator;
        private readonly IDocumentContainer m_documents;

        public DocumentStorage(IStorageRegistration storageRegistration,
            IStorageIdentifier storageIdentifier,
            Func<IDocument> documentCreator,
            IDocumentContainer documents)
        {
            m_storageRegistration = storageRegistration;
            m_storageIdentifier = storageIdentifier;
            m_documentCreator = documentCreator;
            m_documents = documents;
        }

        public bool DocumentExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public IDocument Prepare(string filePath)
        {
            if (!DocumentExists(filePath))
            {
                return null;
            }

            var document = m_documentCreator();
            document.Initialize(filePath);

            m_documents.Clear();
            m_documents.Add(document);

            return document;
        }

        public IDocument Read(string filePath)
        {
            if (!DocumentExists(filePath))
            {
                return null;
            }

            var document = m_documentCreator();
            var stream = new FileStream(filePath, FileMode.Open); // disposed by BinaryReader
            using (var reader = new BinaryReader(stream))
            {
                string name = reader.ReadString();
                int version = reader.ReadInt32();
                DateTime lastModification = DateTime.FromBinary(reader.ReadInt64());
                document.Initialize(new DocumentInfo(filePath, name, version, lastModification));

                int length = reader.ReadInt32();
                for (int i = 0; i < length; ++i)
                {
                    string typeIdentifier = reader.ReadString();
                    var storageProvider = m_storageRegistration.GetStorageProvider(typeIdentifier);
                    if (storageProvider == null)
                    {
                        break;
                    }

                    // typeIdentifier is not read by storage provider
                    if (storageProvider.CreateObject(reader, document) is IIdentifiable @object)
                    {
                        document.Add(typeIdentifier, @object);
                        storageProvider.ReadObjectContent(reader, document, @object);
                    }
                }

                m_storageIdentifier.CallPostSerializationActions(document.Get<IIdentifiableContainer>());
            }
            return document;
        }

        public void Write(IDocument document, string filePath)
        {
            if (DocumentExists(filePath))
            {
                File.Delete(filePath);
            }

            var stream = new FileStream(filePath, FileMode.Create); // disposed by BinaryWriter
            using (var writer = new BinaryWriter(stream))
            {
                document.Initialize(filePath);

                IDocumentInfo documentInfo = document.DocumentInfo;
                writer.Write(documentInfo.Name);
                writer.Write(documentInfo.Version);
                writer.Write(documentInfo.LastModification.ToBinary());

                string[] allTypeIdentifiers = document.AllTypeIdentifiers;
                var storageProviderPairs = new Dictionary<string, IStorageProvider>();

                foreach (string typeIdentifier in allTypeIdentifiers)
                {
                    var storageProvider = m_storageRegistration.GetStorageProvider(typeIdentifier);
                    if (storageProvider == null)
                    {
                        continue;
                    }
                    storageProviderPairs.Add(typeIdentifier, storageProvider);
                }

                writer.Write(storageProviderPairs.Count);

                foreach (var storageProviderPair in storageProviderPairs)
                {
                    // typeIdentifier is written by storage provider
                    IStorageProvider storageProvider = storageProviderPair.Value;
                    string typeIdentifier = storageProviderPair.Key;
                    storageProvider.WriteObject(document.Get(typeIdentifier), writer);
                }

                document.DocumentInfo.UpdateAfterSavingDocument();
            }
        }
    }
}
