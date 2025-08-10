// (c) 2018 Roland Boon

namespace BoonOrg.Identification
{
    public interface IContainerHandover<T> : IContainerHandover where T : IIdentifiable
    {
        void Handover(T source);
    }

    public interface IContainerHandover
    {
        void Handover(object source);
    }
}
