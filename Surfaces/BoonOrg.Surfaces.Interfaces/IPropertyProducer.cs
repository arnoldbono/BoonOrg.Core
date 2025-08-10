// (c) 2023, 2024 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces
{
    public interface IPropertyProducer
    {
        public string Name { get; }

        public IPropertyAttribute Produce(IVector[] vertices);
    }
}
