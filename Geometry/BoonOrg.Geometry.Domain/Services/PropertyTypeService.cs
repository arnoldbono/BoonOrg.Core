// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Domain.Services
{
    internal sealed class PropertyTypeService : IPropertyTypeService
    {
        private readonly List<IPropertyType> m_propertyTypes = new();

        public IEnumerable<IPropertyType> PropertyTypes => m_propertyTypes;

        public IPropertyType FindOrCreate(string name)
        {
            var propertyType = m_propertyTypes.FirstOrDefault(x => x.Name == name);
            if (propertyType != null)
            {
                return propertyType;
            }

            Add(propertyType = new PropertyType(name));
            return propertyType;
        }

        public void Add(IPropertyType propertyType)
        {
            m_propertyTypes.RemoveAll(x => x.Name == propertyType.Name);
            m_propertyTypes.Add(propertyType);
        }
    }
}
