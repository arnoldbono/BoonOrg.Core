// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;

namespace BoonOrg.Geometry.Attributes
{
    public interface IAttributeModifierService
    {
        void InformAttributeChanged(IGeometry geometry);

        T GetOrCreateAttribute<T>(IGeometry geometry, bool createIfNotFound) where T : class, IGeometryAttribute;

        T GetAttribute<T>(IGeometry geometry) where T : class, IGeometryAttribute;

        void UpdateAttribute<T>(IGeometry geometry, T attribute) where T : class, IGeometryAttribute;
    }
}
