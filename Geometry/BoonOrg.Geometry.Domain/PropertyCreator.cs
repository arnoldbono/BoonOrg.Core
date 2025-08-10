// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class PropertyCreator : IPropertyCreator
    {
        public IProperty<T> Create<T>(string name)
        {
            return new Property<T> { Name = name };
        }
    }
}
