// (c) 2017, 2018 Roland Boon

using System;

using BoonOrg.Identification;

namespace BoonOrg.Storage
{
    public interface IIdentifiableStreamWriter : IIdentifiable, IDisposable
    {
        void Open(string path, bool truncate);

        void WriteLine(string line);

        void Write(string line);

        void Close();
    }
}