// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Identification
{
    /// <summary>
    /// An item that is identifiable by name (Name) and/or identifier (Id). 
    /// </summary>
    public interface IIdentity
    {
        Guid Id { get; }

        string Name { get; }

        // The owner of this instance.
        IIdentifiable Owner { get; }

        // The parent of Identifiable.
        IIdentity Parent { get; }

        string Reference { get; }

        void Rename(string name);

        void ResetId(Guid id);

        void SetOwner(IIdentifiable owner);

        void Adopt(IIdentifiable item);

        void Disown(IIdentifiable item);
    }
}
