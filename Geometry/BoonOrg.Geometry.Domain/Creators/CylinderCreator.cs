// (c) 2017 Roland Boon

using BoonOrg.Registrations;
using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class CylinderCreator : ICylinderCreator
    {
        private readonly IResolver m_resolver;

        public CylinderCreator(IResolver resolver)
        {
            m_resolver = resolver;
        }

        public ICylinder Create() => Create(1.0, 1.0, 1.0, 20);

        public ICylinder Create(double radiusX, double radiusY, double height, int resolution)
        {
            var cylinder = m_resolver.Resolve<ICylinder>();
            cylinder.Set(radiusX, radiusY, height, resolution);
            return cylinder;
        }
    }
}
