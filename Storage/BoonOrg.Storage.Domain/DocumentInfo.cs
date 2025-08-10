// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Storage.Domain
{
    internal sealed class DocumentInfo : IDocumentInfo
    {
        private int m_initialVersion;

        public DocumentInfo(string filePath, string name, int version, DateTime lastModification)
        {
            FilePath = filePath;
            Name = name;
            Version = version;
            m_initialVersion = version;
            LastModification = lastModification;
        }

        public string FilePath { get; private set; }

        public string Name { get; set; }

        public int Version { get; set; }

        public DateTime LastModification { get; private set; }

        public bool IsDirty => m_initialVersion != Version;

        public void UpdateAfterModification()
        {
            if (m_initialVersion == Version)
            {
                ++Version;
            }
            LastModification = DateTime.Now;
        }

        public void UpdateAfterSavingDocument()
        {
            m_initialVersion = Version;
        }
    }
}
