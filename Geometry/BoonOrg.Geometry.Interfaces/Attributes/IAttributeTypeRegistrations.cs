// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Attributes
{
    public interface IAttributeTypeRegistrations
    {
        IEnumerable<IAttributeTypeRegistration> Registrations { get; }

        IAttributeTypeRegistration GetRegistration(IGeometryAttribute geometryAttribute);

        IGeometryAttribute Create(string typeName);

        IGeometryAttribute Create(int typeId);

        void Register(Type t);
    }
}