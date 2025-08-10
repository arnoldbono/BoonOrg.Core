// (c) 2017 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Geometry.Creators;
using BoonOrg.Functions;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class VectorMapping : IIdentifiableMapping<IVector>
    {
        private readonly Creators.IVectorCreator m_creator;

        public VectorMapping(Creators.IVectorCreator creator)
        {
            m_creator = creator;
        }

        public bool TryMap(IIdentifiable item, IIdentifiableContainer container, out IVector variable)
        {
            if (item is IToVector vectorItem)
            {
                variable = vectorItem.ToVector(container);
                return true;
            }
            if (item is IToPoint pointItem)
            {
                var p = pointItem.ToPoint(container);
                variable = m_creator.Create(p.X, p.Y, p.Z);
                return true;
            }
            variable = default(IVector);
            return false;
        }
    }
}
