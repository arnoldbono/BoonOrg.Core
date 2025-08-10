// (c) 2023 Roland Boon

using System;

using BoonOrg.Identification;

namespace BoonOrg.Storage.Logic
{
    internal sealed class IdentifiableStreamWriterService : IIdentifiableStreamWriterService
    {
        private readonly Func<IIdentifiableStreamWriter> m_streamWriterFunc;

        public IdentifiableStreamWriterService(Func<IIdentifiableStreamWriter> streamWriterFunc)
        {
            m_streamWriterFunc = streamWriterFunc;
        }

        public void Add(string name, string path, IIdentifiableContainer container)
        {
            var streamWriter = container.FindByName<IIdentifiableStreamWriter>(name);
            if (streamWriter != null)
            {
                container.Remove(streamWriter);
            }

            streamWriter = m_streamWriterFunc();
            streamWriter.Identification.Rename(name);
            container.Add(streamWriter);

            streamWriter.Open(path, true);
        }

        public void Open(string name, string path, IIdentifiableContainer container)
        {
            var streamWriter = container.FindByName<IIdentifiableStreamWriter>(name);
            if (streamWriter != null)
            {
                streamWriter.Open(path, false);
            }
        }

        public void Write(string name, string text, IIdentifiableContainer container)
        {
            var streamWriter = container.FindByName<IIdentifiableStreamWriter>(name);
            if (streamWriter != null)
            {
                streamWriter.Write(text);
            }
        }

        public void WriteLine(string name, string text, IIdentifiableContainer container)
        {
            var streamWriter = container.FindByName<IIdentifiableStreamWriter>(name);
            if (streamWriter != null)
            {
                streamWriter.WriteLine(text);
            }
        }

        public void Close(string name, IIdentifiableContainer container)
        {
            var streamWriter = container.FindByName<IIdentifiableStreamWriter>(name);
            if (streamWriter != null)
            {
                streamWriter.Close();
                container.Remove(streamWriter);
            }
        }
    }
}
