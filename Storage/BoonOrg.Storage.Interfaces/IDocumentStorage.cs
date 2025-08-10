// (c) 2017 Roland Boon

namespace BoonOrg.Storage
{
    public interface IDocumentStorage
    {
        bool DocumentExists(string filePath);

        IDocument Prepare(string filePath);

        IDocument Read(string filePath);

        void Write(IDocument document, string filePath);
    }
}
