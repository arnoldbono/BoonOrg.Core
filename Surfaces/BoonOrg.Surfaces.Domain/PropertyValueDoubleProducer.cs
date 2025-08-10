// (c) 2024 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public sealed class PropertyValueDoubleProducer
    {
        private readonly PropertyProducer<IPropertyValueDouble> m_propertyProducer;

        public PropertyValueDoubleProducer(PropertyProducer<IPropertyValueDouble> propertyProducer)
        {
            m_propertyProducer = propertyProducer;
        }

        public IPropertyAttribute Produce(IVector[] vertices, Func<double, double, double, double> function, string name)
        {
            return m_propertyProducer.Produce(vertices, (x, y, z) => new PropertyValueDouble(function(x, y, z)), name);
        }
    }
}
