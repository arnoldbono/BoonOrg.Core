// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    internal sealed class GeometryEventFactory : IGeometryEventFactory
    {
        public T Create<T>(IEnumerable<IGeometry> items) where T : class, IGeometryEvent
        {
            return typeof(T).Name switch
            {
                nameof(IAttributeChangedEvent) => new AttributeChangedEvent(items) as T,
                nameof(IGeometryAddedEvent) => new GeometryAddedEvent(items) as T,
                nameof(IGeometryDeletedEvent) => new GeometryDeletedEvent(items) as T,
                nameof(IGeometryDocumentCloseEvent) => new GeometryDocumentCloseEvent() as T,
                nameof(IGeometryModifiedEvent) => new GeometryModifiedEvent(items) as T,
                nameof(IGeometrySelectionEvent) => new GeometrySelectionEvent(items) as T,
                _ => null
            };
        }
    }
}
