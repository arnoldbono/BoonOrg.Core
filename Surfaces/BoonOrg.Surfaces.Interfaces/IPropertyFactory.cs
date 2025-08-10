// (c) 2023, 2024 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces
{
    public interface IPropertyFactory
    {
        string[] PropertyNames { get; }

        public IPropertyAttribute Produce(IVector[] vertices, string propertyName);
    }
}
