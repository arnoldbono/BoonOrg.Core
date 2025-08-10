// (c) 1993, 2011, 2016, 2017 Roland Boon

using System;

namespace BoonOrg.Identification.Domain
{
    /// <summary>
    /// All identifiable objects have an identity.
    /// </summary>
    public sealed class Identity : IIdentity
    {
        private string m_name;
        private Guid m_id;
        private IIdentifiable m_owner;

        public string Name { get { return m_name; } set { Rename(value); } }

        public Guid Id { get { return m_id; } set { ResetId(value); } }

        public IIdentifiable Owner { get { return m_owner; } set { SetOwner(value); } }

        public IIdentity Parent { get; set; }

        public string Reference
        {
            get
            {
                // At top level, there is a Collection with an empty name and no parent.
                // All references start with "//".
                // The Reference for the top level Collection instance is "//".
                // The Parent for the top level Collection is |null|.
                IIdentity item = this;
                IIdentity parent = item.Parent;
                if (parent == null)
                {
                    return "//";
                }

                string retval = item.Name;
                while (true)
                {
                    item = parent;
                    if (item.Parent == null)
                    {
                        break;
                    }

                    retval = item.Name + @"/" + retval;
                }

                return @"//" + retval;
            }
        }

        public Identity() : this(string.Empty)
        {
        }

        public Identity(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public void Rename(string name)
        {
            m_name = CleanName(name);
            // TODO Send a Modified event.
        }

        public void ResetId(Guid id)
        {
            m_id = id;
            // TODO Send a Modified event.
        }

        public void SetOwner(IIdentifiable owner)
        {
            if (m_owner != null)
            {
                throw new InvalidOperationException(@"An identity cannot change owners.");
            }

            m_owner = owner ?? throw new ArgumentNullException(@"An identity needs a valid owner.");
        }

        public override string ToString()
        {
            return Name;
        }

        public void Adopt(IIdentifiable item)
        {
            if (item.Identification is Identity concreteItem)
            {
                concreteItem.Parent = this;
            }
        }

        public void Disown(IIdentifiable item)
        {
            if (item.Identification is Identity concreteItem && concreteItem.Parent == this)
            {
                concreteItem.Parent = null;
            }
        }

        public static string CleanName(string name)
        {
            return name?.Replace(@"_", @"").ToLower();
        }
    }
}
