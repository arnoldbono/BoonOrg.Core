// (c) 2019, 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading;

using BoonOrg.Geometry.Events;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.Services
{
    internal sealed class GeometryEventService : IGeometryEventService
    {
        private readonly Subject<IGeometryEvent> m_eventsSubject = new();
        private readonly IGeometryEventFactory m_geometryEventFactory;

        public GeometryEventService(IGeometryEventFactory geometryEventFactory)
        {
            m_geometryEventFactory = geometryEventFactory;
        }

        public IObservable<IGeometryEvent> Events => m_eventsSubject;

        public void Inform(IGeometryEvent geometryEvent)
        {
            // Always invoke events on the main thread. Some objects like BitmapSource can only be accessed on the main thread.
            var context = SynchronizationContext.Current;
            if (context != null)
            {
                context.Post(_ => m_eventsSubject.OnNext(geometryEvent), null);
            }
            else
            {
                m_eventsSubject.OnNext(geometryEvent);
            }
        }

        public void Added(IGeometry item)
        {
            Added(new IGeometry[] { item });
        }

        public void Added(IEnumerable<IGeometry> items)
        {
            var geometryEvent = m_geometryEventFactory.Create<IGeometryAddedEvent>(items);
            Inform(geometryEvent);
        }

        public void Deleted(IGeometry item)
        {
            Deleted(new IGeometry[] { item });
        }

        public void Deleted(IEnumerable<IGeometry> items)
        {
            var geometryEvent = m_geometryEventFactory.Create<IGeometryDeletedEvent>(items);
            Inform(geometryEvent);
        }

        public void Modified(IGeometry item, bool updateVertices, bool updatePropertyValues, bool updateColorset, bool attributeChange)
        {
            Modified(new IGeometry[] { item }, updateVertices, updatePropertyValues, updateColorset, attributeChange);
        }

        public void Modified(IEnumerable<IGeometry> items, bool updateVertices, bool updatePropertyValues, bool updateColorset, bool attributeChange)
        {
            var geometryModifiedEvent = m_geometryEventFactory.Create<IGeometryModifiedEvent>(items);
            geometryModifiedEvent.UpdateVertices = updateVertices;
            geometryModifiedEvent.UpdatePropertyValues = updatePropertyValues;
            geometryModifiedEvent.UpdateColorMap = updateColorset;
            geometryModifiedEvent.AttributeChange = attributeChange;
            Inform(geometryModifiedEvent);
        }

        public void Selected(IGeometry item)
        {
            Selected(new IGeometry[] { item });
        }

        public void Selected(IEnumerable<IGeometry> items)
        {
            var geometryEvent = m_geometryEventFactory.Create<IGeometrySelectionEvent>(items);
            Inform(geometryEvent);
        }

        public void AttributedChanged(IGeometry item)
        {
            AttributedChanged(new IGeometry[] { item });
        }

        public void AttributedChanged(IEnumerable<IGeometry> items)
        {
            var geometryEvent = m_geometryEventFactory.Create<IAttributeChangedEvent>(items);
            Inform(geometryEvent);
        }

        public void DocumentClosed()
        {
            var geometryEvent = m_geometryEventFactory.Create<IGeometryDocumentCloseEvent>(null);
            Inform(geometryEvent);
        }
    }
}
