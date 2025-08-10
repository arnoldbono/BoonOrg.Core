// (c) 2019, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Events;

namespace BoonOrg.Geometry.Services
{
    public interface IGeometryEventService
    {
        IObservable<IGeometryEvent> Events { get; }

        void Inform(IGeometryEvent geometryEvent);

        void Added(IGeometry item);

        void Added(IEnumerable<IGeometry> items);

        void Deleted(IGeometry item);

        void Deleted(IEnumerable<IGeometry> items);

        void Modified(IGeometry item, bool updateVertices, bool updatePropertyValues, bool updateColorset, bool attributeChange);

        void Modified(IEnumerable<IGeometry> items, bool updateVertices, bool updatePropertyValues, bool updateColorMap, bool attributeChange);

        void Selected(IGeometry item);

        void Selected(IEnumerable<IGeometry> items);

        void AttributedChanged(IGeometry item);

        void AttributedChanged(IEnumerable<IGeometry> items);

        void DocumentClosed();
    }
}
