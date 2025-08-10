// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class PropertyType : IPropertyType
    {
        public string Name { get; }

        public PropertyType(string name)
        {
            Name = name;
        }
    }
}
