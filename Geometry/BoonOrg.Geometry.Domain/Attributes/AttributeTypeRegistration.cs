// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Geometry.Attributes;
using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Domain.Attributes
{
    public class AttributeTypeRegistration : IAttributeTypeRegistration
    {
        public AttributeTypeRegistration(string typeName, Func<IGeometryAttribute> func, int typeId)
        {
            TypeName = typeName;
            Func = func;
            TypeId = typeId;
        }

        public string TypeName { get; }

        public Func<IGeometryAttribute> Func { get; }

        public int TypeId { get; }
    }

    public class AttributeTypeRegistrations : IAttributeTypeRegistrations
    {
        private readonly IResolver m_resolver;

        private readonly List<IAttributeTypeRegistration> m_registrations = new();

        public IEnumerable<IAttributeTypeRegistration> Registrations => m_registrations;

        public AttributeTypeRegistrations(IResolver resolver)
        {
            m_resolver = resolver;
        }

        public IAttributeTypeRegistration GetRegistration(IGeometryAttribute geometryAttribute)
        {
            var geometryAttributeType = geometryAttribute.GetType();

            foreach (var registration in m_registrations)
            {
                var registrationType = Type.GetType(registration.TypeName);
                if (registrationType.IsAssignableFrom(geometryAttributeType))
                {
                    return registration;
                }
            }

            return null;
        }

        public void Register(Type t)
        {
            m_registrations.Add(new AttributeTypeRegistration(t.AssemblyQualifiedName, () => (IGeometryAttribute)m_resolver.Resolve(t), m_registrations.Count));
        }

        public IGeometryAttribute Create(string typeName)
        {
            return m_registrations.SingleOrDefault(m => string.Compare(m.TypeName, typeName, true) == 0)?.Func();
        }

        public IGeometryAttribute Create(int typeId)
        {
            return m_registrations.SingleOrDefault(m => m.TypeId == typeId)?.Func();
        }
    }
}
