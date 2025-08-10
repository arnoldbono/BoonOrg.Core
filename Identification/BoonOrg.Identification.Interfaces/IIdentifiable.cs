// (c) 2017 Roland Boon

namespace BoonOrg.Identification
{
    /// <summary>
    /// An item that is identifiable.
    /// </summary>
    public interface IIdentifiable
    {
        IIdentity Identification { get; }
    }
}
