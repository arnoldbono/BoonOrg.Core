// (c) 2023, 2024 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Surfaces.Domain
{
    public class PropertyProducer<T> where T : IPropertyValue
    {
        private readonly Func<IIndexedPropertyAttribute<T>> m_attributeFunc;
        private readonly IPropertyTypeService m_propertyTypeService;

        public PropertyProducer(IPropertyTypeService propertyTypeService, Func<IIndexedPropertyAttribute<T>> attributeFunc)
        {
            m_attributeFunc = attributeFunc;
            m_propertyTypeService = propertyTypeService;
        }

        public IPropertyAttribute Produce(IVector[] vertices, Func<double, double, double, T> function, string name)
        {
            var attribute = m_attributeFunc();
            attribute.PropertyType = m_propertyTypeService.FindOrCreate(name);
            attribute.Generate(vertices, function);
            attribute.Normalize();
            return attribute;
        }
    }
}
