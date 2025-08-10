// (c) 2017 Roland Boon

using System.Collections.Generic;
using System.Linq;

using BoonOrg.Registrations;
using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class CompoundShapeCreator<T> : ICompoundShapeCreator<T> where T : ITriangleContainer
    {
        private readonly IResolver m_resolver;

        public CompoundShapeCreator(IResolver resolver)
        {
            m_resolver = resolver;
        }

        public ICompoundShape<T> Create()
        {
            return Create(Enumerable.Empty<T>(), string.Empty);
        }

        public ICompoundShape<T> Create(IEnumerable<T> containers, string name)
        {
            var shape = m_resolver.Resolve<ICompoundShape<T>>();
            shape.Add(containers);
            shape.Identification.Rename(name);
            return shape;
        }
    }
}
