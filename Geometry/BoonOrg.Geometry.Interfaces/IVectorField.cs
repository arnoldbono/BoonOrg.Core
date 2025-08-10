// (c) 2016, 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Geometry
{
    public interface IVectorField : IIdentifiable
    {
        IVector Evaluate(IPoint point, IIdentifiableContainer container);
    }
}
