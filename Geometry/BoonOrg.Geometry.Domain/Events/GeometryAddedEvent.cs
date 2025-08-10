// (c) 2019 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    internal sealed class GeometryAddedEvent : GeometryEvent, IGeometryAddedEvent
    {
        public GeometryAddedEvent(IEnumerable<IGeometry> items) :
            base(items)
        {
        }
    }
}
