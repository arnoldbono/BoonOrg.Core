// (c) 2018, 2019, 2023 Roland Boon

using System;

namespace BoonOrg.Storage
{
    public interface IDocumentServer
    {
        IObservable<IDocument> Closed { get; }

        IObservable<IDocument> Opened { get; }

        void Initialize(string storageFolder);

        IDocument Document { get; }

        IDocument NewDocument();

        IDocument Get(string name);

        IDocument Prepare(string filePath);

        IDocument Read(string filePath);

        void SaveWithPath(IDocument document, string filePath);

        void SaveWithName(IDocument document, string name);

        void Open(IDocument document);

        void Close(IDocument document);
    }
}
