// (c) 2015, 2016, 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <inheritdoc/>
    public abstract class Surface : ISurface
    {
        private readonly IIdentity m_identity;

        public IIdentity Identification => m_identity;

        public Surface(IIdentity identity)
        {
            m_identity = identity;
            m_identity.SetOwner(this);
        }

        public abstract IBoundingBox GetBoundingBox();

        public abstract void ExpandBoundingBox(IBoundingBox box);
    }
}
