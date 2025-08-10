// (c) 2017, 2018 Roland Boon

using System.IO;

using BoonOrg.Identification;

namespace BoonOrg.Storage.Domain
{
    public sealed class IdentifiableStreamWriter : IIdentifiableStreamWriter
    {
        private StreamWriter m_writer;

        public IIdentity Identification { get; }

        public IdentifiableStreamWriter(IIdentity identity)
        {
            Identification = identity;
        }

        public void Open(string path, bool truncate)
        {
            var folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            m_writer = (truncate || !File.Exists(path)) ? File.CreateText(path) : File.AppendText(path);
        }

        public void WriteLine(string line)
        {
            if (m_writer == null)
            {
                return;
            }
            m_writer.WriteLine(line);
        }

        public void Write(string line)
        {
            if (m_writer == null)
            {
                return;
            }
            m_writer.Write(line);
        }

        public void Close()
        {
            if (m_writer == null)
            {
                return;
            }
            m_writer.Close();
            m_writer.Dispose();
            m_writer = null;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
