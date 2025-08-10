// (c) 2023 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public interface IPropertyContainer
    {
        IEnumerable<IProperty> Properties { get; }

        IProperty GetProperty(string name);

        T GetProperty<T>(string name) where T : IProperty;

        void AddProperty(IProperty value);

        void RemoveProperty(string name);

        void Clear();
    }
}
