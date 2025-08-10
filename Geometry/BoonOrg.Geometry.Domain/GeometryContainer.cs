// (c) 2017, 2018, 2023 Roland Boon

using System.Collections.Generic;
using System.Linq;
using System;

using BoonOrg.Identification;
using BoonOrg.Identification.Domain;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class GeometryContainer : IdentifiableCollection<IGeometry>, IGeometryContainer
    {
        public GeometryContainer(IIdentity identity) : base(identity)
        {
        }

        public bool Exists(string name)
        {
            return string.IsNullOrEmpty(name) ? false : 
                this.Any(s => string.Compare(s.Identification.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        public IGeometry Get(string name)
        {
            return string.IsNullOrEmpty(name) ? null :
                this.FirstOrDefault(s => string.Compare(s.Identification.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        public IGeometry Get(Guid id)
        {
            return this.FirstOrDefault(s => s.Identification.Id == id);
        }

        public string GetUniqueName(string name)
        {
            if (!Exists(name))
            {
                return name;
            }
            int length = name.Length;
            int i = length;
            while (i > 1)
            {
                if (!char.IsDigit(name[i - 1]))
                {
                    break;
                }
                --i;
            }
            int number = 0;
            if (i < length)
            {
                number = int.Parse(name.Substring(i));
                name = name.Substring(0, i - 1).Trim();
            }
            while (true)
            {
                ++number;
                string proposedName = $@"{name} {number}";
                if (!Exists(proposedName))
                {
                    return proposedName;
                }
            }
        }

        public IEnumerable<T> GetList<T>() where T : IGeometry
        {
            return this.OfType<T>();
        }

        public bool ContainsGeometry(IGeometry geometry) => Contains(geometry);

        public void AddGeometry(IGeometry geometry) => Add(geometry);

        public void AddOrReplaceGeometry(IGeometry geometry)
        {
            RemoveGeometry(geometry.Identification.Name);
            AddGeometry(geometry);
        }

        public void RemoveGeometry(IGeometry geometry) => Remove(geometry);

        public void RemoveGeometry(string geometryName)
        {
            var geometry = Get(geometryName);
            if (geometry != null)
            {
                RemoveGeometry(geometry);
            }
        }

        public void Handover(IGeometryContainer source)
        {
            foreach (var geometry in source)
            {
                Add(geometry);
            }
        }

        public void Handover(object source)
        {
            Handover((IGeometryContainer)source);
        }
    }
}
