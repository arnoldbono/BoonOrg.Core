// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Services
{
    public interface IPropertyTypeService
    {
        IEnumerable<IPropertyType> PropertyTypes { get; }

        IPropertyType FindOrCreate(string name);

        void Add(IPropertyType propertyType);
    }
}
