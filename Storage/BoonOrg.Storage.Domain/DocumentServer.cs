// (c) 2018, 2019, 2023 Roland Boon

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace BoonOrg.Storage.Domain
{
    internal sealed class DocumentServer : IDocumentServer
    {
        private readonly Subject<IDocument> m_openedDocument = new();
        private readonly Subject<IDocument> m_closedDocument = new();
        private readonly Func<IDocument> m_documentFunc;
        private readonly IDocumentStorage m_documentStorage;
        private readonly IDocumentContainer m_documentContainer;
        private readonly IEnumerable<IInitialDocumentContentCreator> m_initialDocumentContentCreators;
        private string m_storageFolder;
        private IDocument m_activeDocument;

        public IObservable<IDocument> Opened => m_openedDocument;

        public IObservable<IDocument> Closed => m_closedDocument;

        public IDocument Document => m_activeDocument;

        public DocumentServer(Func<IDocument> documentCreator,
            IDocumentStorage documentStorage,
            IDocumentContainer documentContainer,
            IEnumerable<IInitialDocumentContentCreator> initialDocumentContentCreators)
        {
            m_documentFunc = documentCreator;
            m_documentStorage = documentStorage;
            m_documentContainer = documentContainer;
            m_initialDocumentContentCreators = initialDocumentContentCreators;
        }

        public void Initialize(string storageFolder)
        {
            m_storageFolder = storageFolder;
        }
         
        public IDocument NewDocument()
        {
            var document = m_documentFunc();
            AddDocument(document);
            return document;
        }

        private void AddDocument(IDocument document)
        {
            AddInitialDocumentContent(document);
            if (m_documentContainer.Contains(document))
            {
                m_documentContainer.Add(document);
            }
            m_activeDocument = document;
            m_openedDocument.OnNext(document);
        }

        public IDocument Get(string name)
        {
            return m_documentContainer.FirstOrDefault(d => d.DocumentInfo.Name == name);
        }

        private void AddInitialDocumentContent(IDocument document)
        {
            if (document == null)
            {
                return;
            }

            foreach (var creator in m_initialDocumentContentCreators)
            {
                document.AddIfMissing(creator.Create());
            }
        }

        public IDocument Prepare(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            var document = m_documentStorage.Prepare(filePath);
            if (document != null)
            {
                AddDocument(document);
            }

            return document;
        }

        public IDocument Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            var document = m_documentStorage.Read(filePath);
            if (document != null)
            {
                AddDocument(document);
            }

            return document;
        }

        public void SaveWithPath(IDocument document, string filePath)
        {
            if (document == null || string.IsNullOrEmpty(filePath))
            {
                return;
            }

            m_documentStorage.Write(document, filePath);
        }

        public void SaveWithName(IDocument document, string name)
        {
            if (document == null || string.IsNullOrEmpty(name))
            {
                return;
            }

            var filePath = Path.Combine(m_storageFolder, $@"{name}.borg");
            SaveWithPath(document, filePath);
        }

        public void Open(IDocument document)
        {
            if (document == null)
            {
                return;
            }

            AddDocument(document);

            m_openedDocument.OnNext(document);
        }

        public void Close(IDocument document)
        {
            if (document == null)
            {
                return;
            }

            if (m_activeDocument == document)
            {
                m_activeDocument = null;
            }

            m_documentContainer.Remove(document);

            document.Close();
            m_closedDocument.OnNext(document);
        }
    }
}
