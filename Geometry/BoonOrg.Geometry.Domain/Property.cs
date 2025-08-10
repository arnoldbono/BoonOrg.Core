// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Domain
{
    public class Property<T> : IProperty<T>
    {
        private readonly List<T> m_values = new();

        public T this[int index] { get => m_values[index]; set => m_values[index] = value; }

        public IEnumerable<T> Values => m_values.ToArray();

        public string Name { get; set; }

        public int Count => m_values.Count;

        public void Add(T value)
        {
            m_values.Add(value);
        }

        public void AddRange(IEnumerable<T> values)
        {
            m_values.AddRange(values);
        }

        public void Clear() => m_values.Clear();
    }
}
