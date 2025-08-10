// (c) 2019, 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    public class GeometryEvent : IGeometryEvent
    {
        public GeometryEvent(IEnumerable<IGeometry> items)
        {
            Items = items;
        }

        public IEnumerable<IGeometry> Items { get; }
    }
}
