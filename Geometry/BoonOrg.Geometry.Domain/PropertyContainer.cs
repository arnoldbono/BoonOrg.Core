// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

namespace BoonOrg.Geometry.Domain
{
    public class PropertyContainer : IPropertyContainer
    {
        protected readonly List<IProperty> m_properties = new();

        public IEnumerable<IProperty> Properties => m_properties.ToArray();

        public IProperty GetProperty(string name) => m_properties.SingleOrDefault(p => p.Name == name);

        public T GetProperty<T>(string name) where T : IProperty => m_properties.OfType<T>().SingleOrDefault(p => p.Name == name);

        public void AddProperty(IProperty property)
        {
            var item = GetProperty(property.Name);
            if (item != null)
            {
                m_properties.Remove(item);
            }

            m_properties.Add(property);
        }

        public void RemoveProperty(string name)
        {
            var item = GetProperty(name);
            if (item != null)
            {
                m_properties.Remove(item);
            }
        }

        public void Clear()
        {
            m_properties.Clear();
        }
    }
}
