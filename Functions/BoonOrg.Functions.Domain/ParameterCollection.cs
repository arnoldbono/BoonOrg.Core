// (c) 2017, 2025 Roland Boon

using System;
using System.IO;
using System.Collections.Generic;

using BoonOrg.Identification;
using BoonOrg.Identification.Domain;
using BoonOrg.Registrations;
using BoonOrg.Functions.Creators;

namespace BoonOrg.Functions.Domain
{
    internal sealed class ParameterCollection : IdentifiableCollection<IParameter>, IParameterCollection
    {
        private readonly IResolver m_resolver;
        private readonly IParameterCreator m_parameterCreator;

        public ParameterCollection(IIdentity identity, IResolver resolver, IParameterCreator parameterCreator) : base(identity)
        {
            m_resolver = resolver;
            m_parameterCreator = parameterCreator;
        }

        public IParameter GetParameter(string name)
        {
            name = Identity.CleanName(name);
            return FindByName(this, name, true);
        }

        public void AddParameter(string parameterName, IIdentifiable identifiable)
        {
            Add(m_parameterCreator.Create(parameterName, identifiable));
        }

        public void AddParameter(string parameterName, string parameterValue)
        {
            Add(m_parameterCreator.Create(parameterName, parameterValue));
        }

        public T FindReference<T>(IParameter parameter) where T : class, IIdentifiable
        {
            if (string.IsNullOrEmpty(parameter?.Text))
            {
                return default(T);
            }
            return FindReferal(parameter.Text) as T;
        }

        public T FindReference<T>(string name) where T : class, IIdentifiable
        {
            return FindReference<T>(GetParameter(name));
        }

        public IIdentifiable FindReference(string name)
        {
            return FindReference<IIdentifiable>(GetParameter(name));
        }

        public bool Get<T>(string name, out T value) where T : class, IIdentifiable
        {
            return Get(GetParameter(name), out value);
        }

        public bool Get<T>(IParameter parameter, out T value) where T: class, IIdentifiable
        {
            if (parameter == null)
            {
                value = default(T);
                return false;
            }
            if (!GetValue(parameter, out value))
            {
                value = FindReference<T>(parameter);
            }
            return value != null;
        }

        public bool GetBool(string name, out bool value)
        {
            return GetValue(name, out value);
        }

        public bool GetString(string name, out string value)
        {
            return GetValue(name, out value);
        }

        public bool GetPath(string name, out string value)
        {
            var result = GetValue(name, out value);
            if (result)
            {
                if (value.StartsWith('~'))
                {
                    var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    value = Path.Combine(homeDirectory, value.Substring(1));
                }

                value = Path.GetFullPath(value);
            }
            return result;
        }

        public bool GetInt(string name, out int value)
        {
            return GetValue(name, out value);
        }

        public bool GetDouble(string name, out double value)
        {
            return GetValue(name, out value);
        }

        public bool GetValue<T>(string name, out T value)
        {
            return GetValue(GetParameter(name), out value);
        }

        public bool GetValue<T>(IParameter parameter, out T value)
        {
            return Get<T>(parameter, null, out value);
        }

        public bool Get<T>(string name, IIdentifiableContainer container, out T value)
        {
            var parameter = GetParameter(name) ?? container?.FindByName<IParameter>(name);
            return Get<T>(parameter, container, out value);
        }

        public bool Get<T>(IParameter parameter, IIdentifiableContainer container, out T value)
        {
            value = default(T);
            if (parameter == null)
            {
                return false;
            }

            string text = parameter.FindText();
            var item = parameter.FindValue() ?? FindReference(text) ?? container?.FindByName<IIdentifiable>(text);

            while (item is IParameter parameterAtDeeperLevel)
            {
                item = parameterAtDeeperLevel.FindValue();
            }

            if (item == null && !string.IsNullOrEmpty(text))
            {
                if (m_resolver.TryResolve(out IParameterTextMap<T> typedMapper) && typedMapper.TryMap(text, container, out value))
                {
                    UpdateParameterAndReturn(parameter, value);
                    return true;
                }

                foreach (var mapper in m_resolver.Resolve<IEnumerable<IParameterTextMap>>())
                {
                    if (mapper.TryMap(text, container, out item))
                    {
                        break;
                    }
                }
            }

            if (item != null)
            {
                if (item is not T t &&
                    (!m_resolver.TryResolve(out IIdentifiableMapping<T> mapping) ||
                     !mapping.TryMap(item, container, out t)))
                {
                    return false;
                }

                value = UpdateParameterAndReturn(parameter, t);
                return true;
            }

            if (!string.IsNullOrEmpty(text) && m_resolver.TryResolve(out IParameterTextTryParse<T> parser))
            {
                return parser.TryParse(text, out value);
            }

            return false;
        }

        public bool GetIdentifiable<T>(string name, IIdentifiableContainer container, out T value) where T : class, IIdentifiable
        {
            return GetIdentifiableOrCreate(name, container, false, out value);
        }

        public T GetIdentifiableOrCreate<T>(string name, IIdentifiableContainer container) where T : class, IIdentifiable
        {
            GetIdentifiableOrCreate(name, container, true, out T value);
            return value;
        }

        private bool GetIdentifiableOrCreate<T>(string name, IIdentifiableContainer container, bool createIfNotFound, out T value) where T : class, IIdentifiable
        {
            // See if there is a parameter with this name and this type.
            if (GetValue(name, out value))
            {
                return true;
            }
            if (Get(name, out value))
            {
                return true;
            }
            // See if there is a parameter refering to a container entry else use the given name.
            if (GetString(name, out string targetName) && !string.IsNullOrEmpty(targetName))
            {
                name = targetName;
            }
            value = container.FindByName<T>(name);
            if (value == null && createIfNotFound)
            {
                value = m_resolver.Resolve<T>();
                value.Identification.Rename(name);
                container.Add(value);
            }
            return value != null;
        }

        private static T UpdateParameterAndReturn<T>(IParameter parameter, T t)
        {
            if (t is IIdentifiable i)
            {
                parameter.Value = i;
            }
            return t;
        }
    }
}
