// (c) 2019 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    internal sealed class GeometryDeletedEvent : GeometryEvent, IGeometryDeletedEvent
    {
        public GeometryDeletedEvent(IEnumerable<IGeometry> items) :
            base(items)
        {
        }
    }
}
