// (c) 2024 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces
{
    /// <summary>
    /// Return the property value for the given vertex index.
    /// </summary>
    public interface IIndexedPropertyAttribute<T> : IPropertyAttribute where T : IPropertyValue
    {
        T MinValue { get; }

        T MaxValue { get; }

        T this[int index] { get; set; }

        int[] Indices { get; }

        T[] Values { get; set; }

        void Generate(IVector[] vertices, Func<double, double, double, T> function);

        void Normalize();
    }
}
