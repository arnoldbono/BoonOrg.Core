// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry.Attributes
{
    public interface IAttributeTypeRegistration
    {
        Func<IGeometryAttribute> Func { get; }

        int TypeId { get; }

        string TypeName { get; }
    }
}