// (c) 2017 Roland Boon

using BoonOrg.Registrations;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class BarCreator : IBarCreator
    {
        private readonly IResolver m_resolver;

        public BarCreator(IResolver resolver)
        {
            m_resolver = resolver;
        }

        public IBar Create() => Create(new Point(), 1.0, 1.0, 3.0);

        public IBar Create(IPoint center, double width, double length, double height)
        {
            var bar = m_resolver.Resolve<IBar>();
            bar.Set(center, width, length, height);
            return bar;
        }
    }
}
