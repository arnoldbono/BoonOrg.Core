// (c) 2026 Roland Boon

namespace BoonOrg.Storage
{
    public interface IFilePathCleanup
    {
        string Cleanup(IDocument document, string filePath);

        bool FileExists(string filePath);
    }
}
