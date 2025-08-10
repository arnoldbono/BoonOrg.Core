// (c) 2017, 2018, 2022 Roland Boon

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

using BoonOrg.Identification;
using BoonOrg.Registrations;

namespace BoonOrg.Storage.Domain
{
    internal sealed class Document : IDocument
    {
        private readonly Dictionary<string, IIdentifiable> m_repository = new Dictionary<string, IIdentifiable>();
        private readonly IStorageIdentifier m_storageIdentifier;
        private readonly IResolver m_resolver;

        public IDocumentInfo DocumentInfo { get; private set; }

        public string[] AllTypeIdentifiers => m_repository.Keys.ToArray();

        public Document(IStorageIdentifier storageIdentifier, IResolver resolver)
        {
            m_storageIdentifier = storageIdentifier;
            m_resolver = resolver;
        }

        public void Initialize(string filePath)
        {
            SetDefaultDocumentInfo(filePath);
        }

        public void Initialize(IDocumentInfo documentInfo)
        {
            if (DocumentInfo == null)
            {
                DocumentInfo = documentInfo;
            }
        }

        public void Close()
        {
            m_repository.Clear();
            SetDefaultDocumentInfo(null);
        }

        public void AddIfMissing(IIdentifiable @object)
        {
            string typeIdentifier = m_storageIdentifier.GetTypeIdentifier(@object.GetType());
            if (!m_repository.ContainsKey(typeIdentifier))
            {
                m_repository.Add(typeIdentifier, @object);
            }
        }

        public void Add<T>(T @object) where T : IIdentifiable
        {
            Add(m_storageIdentifier.GetTypeIdentifier(typeof(T)), @object);
        }

        public void Add(string typeIdentifier, IIdentifiable @object)
        {
            if (m_repository.ContainsKey(typeIdentifier))
            {
                m_repository.Remove(typeIdentifier);
            }
            m_repository.Add(typeIdentifier, @object);
        }

        public T Get<T>() where T : IIdentifiable
        {
            string typeIdentifier = m_storageIdentifier.GetTypeIdentifier(typeof(T));
            if (!m_repository.ContainsKey(typeIdentifier))
            {
                return default(T);
            }
            return (T)m_repository[typeIdentifier];
        }

        public T GetOrCreate<T>() where T : IIdentifiable
        {
            string typeIdentifier = m_storageIdentifier.GetTypeIdentifier(typeof(T));
            if (!m_repository.ContainsKey(typeIdentifier))
            {
                var t = m_resolver.Resolve<T>();
                m_repository.Add(typeIdentifier, t);
                return t;
            }

            return (T)m_repository[typeIdentifier];
        }

        public IIdentifiable Get(string typeIdentifier)
        {
            if (!m_repository.ContainsKey(typeIdentifier))
            {
                return null;
            }
            return m_repository[typeIdentifier];
        }

        private void SetDefaultDocumentInfo(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = @"Untitled.borg";
            }
            if (DocumentInfo == null || string.Compare(DocumentInfo.FilePath, filePath, false) != 0)
            {
                DocumentInfo = new DocumentInfo(filePath,
                    Path.GetFileNameWithoutExtension(filePath), 1, DateTime.Now);
            }
        }
    }
}
