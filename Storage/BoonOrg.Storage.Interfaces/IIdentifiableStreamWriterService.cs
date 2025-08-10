// (c) 2023 Roland Boon

using System;

using BoonOrg.Identification;

namespace BoonOrg.Storage
{
    public interface IIdentifiableStreamWriterService
    {
        void Add(string name, string path, IIdentifiableContainer container);

        void Open(string name, string path, IIdentifiableContainer container);

        void Write(string name, string text, IIdentifiableContainer container);

        void WriteLine(string name, string text, IIdentifiableContainer container);

        void Close(string name, IIdentifiableContainer container);
    }
}
