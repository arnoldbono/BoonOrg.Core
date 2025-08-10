// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Storage
{
    public interface IDocumentInfo
    {
        string FilePath { get; }

        string Name { get; }

        int Version { get; }

        DateTime LastModification { get; }

        bool IsDirty { get; }

        void UpdateAfterModification();

        void UpdateAfterSavingDocument();
    }
}
