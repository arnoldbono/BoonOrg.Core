// (c) 2023 Roland Boon

using System;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.Modifiers
{
    internal class AttributeModifierBase
    {
        public IGeometryLocatorService GeometryLocatorService { get; }

        public IAttributeModifierService AttributeModifierService { get; }

        public AttributeModifierBase(IAttributeModifierService attributeModifierService,
            IGeometryLocatorService geometryLocatorService)
        {
            AttributeModifierService = attributeModifierService;
            GeometryLocatorService = geometryLocatorService;
        }

        public IGeometry Find(Guid geometryId)
        {
            return GeometryLocatorService.Find<IGeometry>(geometryId);
        }

        public void InformAttributeChanged(Guid geometryId)
        {
            InformAttributeChanged(Find(geometryId));
        }

        private void InformAttributeChanged(IGeometry geometry)
        {
            AttributeModifierService.InformAttributeChanged(geometry);
        }

        public T GetAttribute<T>(IGeometry geometry) where T : class, IGeometryAttribute
        {
            return AttributeModifierService.GetAttribute<T>(geometry);
        }

        public void UpdateAttribute<T>(IGeometry geometry, T attribute) where T : class, IGeometryAttribute
        {
            AttributeModifierService.UpdateAttribute(geometry, attribute);
        }
    }
}
