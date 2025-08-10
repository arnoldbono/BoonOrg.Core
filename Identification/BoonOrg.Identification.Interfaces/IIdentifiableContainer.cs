// (c) 2017, 2018 Roland Boon

namespace BoonOrg.Identification
{
    public interface IIdentifiableContainer : IIdentifiableCollection<IIdentifiable>, IContainerHandover<IIdentifiableContainer>
    {
        T FindByName<T>(string name) where T : class, IIdentifiable;

        IIdentifiableCollection<IIdentifiable> Root { get; }
    }
}
