// (c) 2016, 2017, 2018, 2023 Roland Boon

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace BoonOrg.Identification.Domain
{
    public class IdentifiableCollection<T> : IIdentifiableCollection<T> where T : class, IIdentifiable
    {
        private readonly List<T> m_items = new();

        public IIdentity Identification { get; }

        public int Count => m_items.Count;

        public bool IsReadOnly => false;

        protected IdentifiableCollection(IIdentity identity)
        {
            Identification = identity;
            Identification.SetOwner(this);
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            m_items.Add(item);
            Identification.Adopt(item);
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            m_items.Remove(item);
            Identification.Disown(item);
            return true;
        }

        public void Clear()
        {
            foreach (T child in m_items)
            {
                Identification.Disown(child);
            }
            foreach (var child in m_items.OfType<IDisposable>())
            {
                child.Dispose();
            }
            m_items.Clear();
        }

        public T FindReferal(string referal)
        {
            IIdentifiable retval = null;

            bool root = referal.StartsWith("//");
            IIdentity item = Identification;

            while (true)
            {
                if (item.Parent == null)
                {
                    break;
                }

                if (referal.StartsWith("../"))
                {
                    referal = referal.Substring(3);
                }
                else
                {
                    break;
                }

                item = item.Parent;
            }

            if (root)
            {
                referal = referal.Substring(2);
            }

            if (item.Parent == null)
            {
                root = true;
            }

            while (root || item.Parent != null)
            {
                int index = referal.IndexOf('/');
                string name = referal;

                if (index >= 0)
                {
                    name = referal.Substring(0, index);
                    referal = referal.Substring(index + 1);
                }

                if (root)
                {
                    item = Identification;
                    while (item != null && item.Parent != null)
                    {
                        item = item.Parent;
                    }
                    root = false;
                }

                if (!(item.Owner is IIdentifiableContainer container))
                {
                    break; // not found
                }

                retval = FindByName(container, name, false);
                if (retval == null)
                {
                    break; // not found
                }

                if (index < 0)
                {
                    break;
                }

                item = retval.Identification;
            }

            return retval as T;
        }

        public T FindByName(IEnumerable<IIdentifiable> items, string name, bool recursive)
        {
            if (items == null)
            {
                return null;
            }

            if (items.FirstOrDefault(c => NameEquals(c.Identification.Name, name)) is T retval)
            {
                return retval;
            }

            if (!recursive)
            {
                return null;
            }

            foreach (T item in items)
            {
                if (item is IIdentifiableContainer container)
                {
                    retval = FindByName(container, name, recursive);
                    if (retval != null)
                    {
                        return retval;
                    }
                }
            }

            return null;
        }

        public T Find(string name)
        {
            return FindItem(name, this);
        }

        public T FindItem(string name, IIdentifiable parent)
        {
            if (NameEquals(parent.Identification.Name, name))
            {
                return parent as T;
            }

            if (!(parent is IIdentifiableContainer container))
            {
                return null;
            }

            foreach (IIdentifiable item in container)
            {
                T childItem = FindItem(name, item);
                if (childItem != null)
                {
                    return childItem;
                }
            }

            return null;
        }

        public static bool NameEquals(string name1, string name2)
        {
            name1 = Identity.CleanName(name1);
            name2 = Identity.CleanName(name2);
            return string.Compare(name1, name2, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public bool Contains(T item)
        {
            return m_items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_items.GetEnumerator();
        }
    }
}
