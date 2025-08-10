// (c) 2017, 2022 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Storage
{
    public interface IDocument
    {
        string[] AllTypeIdentifiers { get; }

        IDocumentInfo DocumentInfo { get; }

        void AddIfMissing(IIdentifiable @object);

        void Initialize(string filePath);

        void Initialize(IDocumentInfo documentInfo);

        void Close();

        void Add<T>(T @object) where T : IIdentifiable;

        void Add(string typeIdentifier, IIdentifiable @object);

        T Get<T>() where T : IIdentifiable;

        T GetOrCreate<T>() where T : IIdentifiable;

        IIdentifiable Get(string typeIdentifier);
    }
}
