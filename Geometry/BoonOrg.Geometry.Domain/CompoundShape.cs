// (c) 2015, 2016, 2017, 2018 Roland Boon

using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    /// <inheritdoc/>
    internal class CompoundShape<T> : Surface, ICompoundShape<T> where T : ITriangleContainer
    {
        protected readonly List<T> m_containers = new List<T>();

        public IEnumerable<T> Containers => m_containers;

        public CompoundShape(IIdentity identity) : base(identity)
        {
        }

        public void Add(IEnumerable<T> containers)
        {
            m_containers.AddRange(containers);
        }

        public override IBoundingBox GetBoundingBox()
        {
            var box = new BoundingBox();
            foreach (T t in m_containers)
            {
                box.Expand(t.Vertices);
            }
            return box;
        }

        public override void ExpandBoundingBox(IBoundingBox box)
        {
            foreach (T t in m_containers)
            {
                box.Expand(t.Vertices);
            }
        }
    }
}
