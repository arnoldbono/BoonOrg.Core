// (c) 2023, 2024 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public class TextureMapPropertyProducer : IPropertyProducer
    {
        private readonly PropertyProducer<IPropertyValueTextureMap> m_propertyProducer;

        public string Name => @"Texture Map";

        public TextureMapPropertyProducer(PropertyProducer<IPropertyValueTextureMap> propertyProducer)
        {
            m_propertyProducer = propertyProducer;
        }

        public IPropertyAttribute Produce(IVector[] vertices)
        {
            return m_propertyProducer.Produce(vertices, (x, y, z) => new PropertyValueTextureMap(x, y), Name);
        }
    }
}
