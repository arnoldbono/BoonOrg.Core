// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public interface IProperty
    {
        string Name { get; set; }

        int Count { get; }

        void Clear();
    }

    public interface IProperty<T> : IProperty
    {
        IEnumerable<T> Values { get; }

        T this[int index] { get; set; }

        void Add(T value);

        void AddRange(IEnumerable<T> values);
    }
}
