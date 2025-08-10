// (c) 2017 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Functions;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class PointMapping : IIdentifiableMapping<IPoint>
    {
        public PointMapping()
        {
        }

        public bool TryMap(IIdentifiable item, IIdentifiableContainer container, out IPoint variable)
        {
            if (item is IToVector vectorItem)
            {
                var v = vectorItem.ToVector(container);
                variable = v.ToPoint();
                return true;
            }
            if (item is IToPoint pointItem)
            {
                variable = pointItem.ToPoint(container);
                return true;
            }
            variable = default(IPoint);
            return false;
        }
    }
}
