// (c) 2023, 2024 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public class PropertyFactory : IPropertyFactory
    {
        private readonly IEnumerable<IPropertyProducer> m_propertyProducers;

        public PropertyFactory(IEnumerable<IPropertyProducer> propertyProducers)
        {
            m_propertyProducers = propertyProducers;
        }

        public string[] PropertyNames
        {
            get
            {
                var properties = new List<string>();
                foreach (var propertyProducer in m_propertyProducers)
                {
                    properties.Add(propertyProducer.Name);
                }

                return properties.ToArray();
            }
        }

        public IPropertyAttribute Produce(IVector[] vertices, string propertyName)
        {
            foreach (var propertyProducer in m_propertyProducers)
            {
                if (propertyProducer.Name != propertyName)
                {
                    continue;
                }

                return propertyProducer.Produce(vertices);
            }

            return null;
        }
    }
}
