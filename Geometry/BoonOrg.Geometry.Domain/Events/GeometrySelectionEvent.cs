// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    internal sealed class GeometrySelectionEvent : GeometryEvent, IGeometrySelectionEvent
    {
        public GeometrySelectionEvent(IEnumerable<IGeometry> items) :
            base(items)
        {
        }
    }
}
