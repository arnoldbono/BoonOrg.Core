// (c) 2024 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public sealed class IndexedPropertyAttribute<TI> : IIndexedPropertyAttribute<TI> where TI : IPropertyValue
    {
        private readonly Dictionary<int, TI> m_valueMap = new();

        public IPropertyType PropertyType { get; set; }

        public TI MinValue { get; private set; }

        public TI MaxValue { get; private set; }

        // For constructor injection
        public IndexedPropertyAttribute(Func<TI> func)
        {
            MinValue = func();
            MaxValue = func();
        }

        public void Generate(IVector[] vertices, Func<double, double, double, TI> function)
        {
            m_valueMap.Clear();
            MinValue.ResetToMaxValue();
            MaxValue.ResetToMinValue();
            var i = 0;
            foreach (var vertex in vertices)
            {
                var mapping = function(vertex.X, vertex.Y, vertex.Z);
                mapping.Update(MinValue, MaxValue);
                this[i++] = mapping;
            }
        }

        public void Normalize()
        {
            int count = m_valueMap.Count;
            for (var i = 0; i < count; ++i)
            {
                m_valueMap[i].Normalize(MinValue, MaxValue);
            }

            // Keep the MinValue and MaxValue to restore the values.
        }

        public TI this[int index]
        {
            get => m_valueMap[index];
            set
            {
                m_valueMap[index] = value;
            }
        }

        public int[] Indices
        {
            get => m_valueMap.Keys.ToArray();
        }

        public TI[] Values
        {
            get => m_valueMap.Values.ToArray();
            set
            {
                m_valueMap.Clear();
                var i = 0;
                foreach (var v in value)
                {
                    m_valueMap.Add(i++, v);
                }
            }
        }
    }
}
