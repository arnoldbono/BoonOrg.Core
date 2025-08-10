// (c) 2016, 2017, 2018 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Identification.Domain;

namespace BoonOrg.Functions.Domain
{
    internal sealed class Parameter : IParameter
    {
        private readonly IIdentity m_identity;

        public string Text { get; set; }

        public IIdentifiable Value { get; set; }

        public IIdentity Identification => m_identity;

        public Parameter(IIdentity identity)
        {
            m_identity = identity;
            m_identity.SetOwner(this);
        }

        public void Set(string name)
        {
            Set(name, name);
        }

        public void Set(string name, IIdentifiable value)
        {
            Set(name, name);
            Value = value;
        }

        public void Set(string name, string referal)
        {
            Identification.Rename(name);
            Text = Identity.CleanName(referal);
        }

        public IIdentifiable FindValue()
        {
            IParameter parameter = this;
            while (parameter.Value is IParameter parameterAtDeeperLevel)
            {
                parameter = parameterAtDeeperLevel;
            }
            return parameter.Value;
        }

        public string FindText()
        {
            IParameter parameter = this;
            while (parameter.Value is IParameter parameterAtDeeperLevel)
            {
                parameter = parameterAtDeeperLevel;
            }
            return parameter.Text;
        }
    }
}
