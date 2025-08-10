// (c) 2019, 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    internal sealed class GeometryModifiedEvent : GeometryEvent, IGeometryModifiedEvent
    {
        public GeometryModifiedEvent(IEnumerable<IGeometry> items) :
            base(items)
        {
        }

        public bool UpdateVertices { get; set; }

        public bool UpdatePropertyValues { get; set; }

        public bool UpdateColorMap { get; set; }

        public bool AttributeChange { get; set; }
    }
}
