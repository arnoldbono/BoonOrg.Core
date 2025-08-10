// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry
{
    public interface IPropertyCreator
    {
        IProperty<T> Create<T>(string name);
    }
}
