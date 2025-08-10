// (c) 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <inheritdoc/>
    public class GeometryItem : IGeometryItem
    {
        private readonly IIdentity m_identity;

        public IIdentity Identification => m_identity;

        public GeometryItem(IIdentity identity)
        {
            m_identity = identity;
            m_identity.SetOwner(this);
        }
    }
}
