// (c) 2024 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces
{
    public interface IPropertyValueDouble : IPropertyValue
    {
        public double Value { get; }
    }
}
