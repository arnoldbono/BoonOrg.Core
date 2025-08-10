// (c) 2023 Roland Boon

using System;

using BoonOrg.DrawingTools.Rendering;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Services;

namespace BoonOrg.DrawingTools.Services
{
    internal sealed class AttributeModifierService : IAttributeModifierService
    {
        private readonly IGeometryEventService m_geometryEventsService;
        private readonly IAttributeTypeRegistrations m_attributeTypeRegistrations;
        private readonly IViewContainer m_viewContainer;

        public AttributeModifierService(IGeometryEventService geometryEventsService,
            IAttributeTypeRegistrations attributeTypeRegistrations,
            IViewContainer viewContainer)
        {
            m_geometryEventsService = geometryEventsService;
            m_attributeTypeRegistrations = attributeTypeRegistrations;
            m_viewContainer = viewContainer;
        }

        public void InformAttributeChanged(IGeometry geometry)
        {
            m_geometryEventsService.AttributedChanged(geometry);
        }

        public T GetOrCreateAttribute<T>(IGeometry geometry, bool createIfNotFound) where T : class, IGeometryAttribute
        {
            var geometryAttributes = GeometryAttributeContainer;
            if (geometryAttributes == null)
            {
                return default;
            }

            var attribute = geometryAttributes.GetAttribute<T>(geometry);
            if (attribute == null && createIfNotFound)
            {
                attribute = (T)m_attributeTypeRegistrations.Create(typeof(T).AssemblyQualifiedName);
                geometryAttributes.AddAttribute(geometry, attribute);
            }

            return attribute;
        }

        public T GetAttribute<T>(IGeometry geometry) where T : class, IGeometryAttribute
        {
            return GetOrCreateAttribute<T>(geometry, false);
        }

        public void UpdateAttribute<T>(IGeometry geometry, T attribute) where T : class, IGeometryAttribute
        {
            GeometryAttributeContainer?.UpdateAttribute(geometry, attribute);

            InformAttributeChanged(geometry);
        }

        private IGeometryAttributeContainer GeometryAttributeContainer
        {
            get
            {
                var view = m_viewContainer.ActiveView;
                return view?.Attributes;
            }
        }
    }
}
