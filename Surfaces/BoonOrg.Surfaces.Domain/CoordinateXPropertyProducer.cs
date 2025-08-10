// (c) 2023, 2024 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public class CoordinateXPropertyProducer : IPropertyProducer
    {
        private readonly PropertyValueDoubleProducer m_propertyProducer;

        public string Name => @"Coordinate x";

        public CoordinateXPropertyProducer(PropertyValueDoubleProducer propertyProducer)
        {
            m_propertyProducer = propertyProducer;
        }

        public IPropertyAttribute Produce(IVector[] vertices)
        {
            return m_propertyProducer.Produce(vertices, (x, y, z) => x, Name);
        }
    }
}
