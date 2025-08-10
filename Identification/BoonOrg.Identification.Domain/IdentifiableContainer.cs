// (c) 1993, 2011, 2016, 2017, 2018 Roland Boon

using System.Linq;

namespace BoonOrg.Identification.Domain
{
    public class IdentifiableContainer : IdentifiableCollection<IIdentifiable>, IIdentifiableContainer
    {
        public IdentifiableContainer(IIdentity identity) : base(identity)
        {
        }

        public T FindByName<T>(string name) where T : class, IIdentifiable
        {
            return FindByName(this, name, true) as T;
        }

        public IIdentifiableCollection<IIdentifiable> Root
        {
            get
            {
                IIdentifiableCollection<IIdentifiable> collection = this;
                while (collection.Identification.Parent?.Owner is IIdentifiableCollection<IIdentifiable> parentCollection)
                {
                    collection = parentCollection;
                }
                return collection;
            }
        }

        public bool IsNameUnique(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            return this.All(m => !NameEquals(m.Identification.Name, name));
        }

        public void NameUnique(IIdentifiable item)
        {
            string name = item.Identification.Name;
            if (IsNameUnique(name))
            {
                return;
            }
            int index = this.Count(m => m.GetType() == item.GetType());
            while (true)
            {
                ++index;
                name = GetTypeNameBasedName(item.GetType().Name, index);
                if (IsNameUnique(name))
                {
                    item.Identification.Rename(name);
                    break;
                }
            }
        }

        private static string GetTypeNameBasedName(string typeName, int index)
        {
            string retval = typeName;
            if (typeName.EndsWith(@"3D"))
            {
                retval = typeName.Substring(0, typeName.Length - 2);
            }
            retval += index.ToString();
            return retval;
        }

        public void Handover(IIdentifiableContainer source)
        {
            var items = source.ToList();
            foreach (var item in items)
            {
                var existingItem = FindByName(this, item.Identification.Name, false);
                if (existingItem != null)
                {
                    Remove(existingItem);
                }
                source.Remove(item);
                Add(item);
            }
        }

        public void Handover(object source)
        {
            Handover((IIdentifiableContainer)source);
        }
    }
}
