// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    internal sealed class AttributeChangedEvent : GeometryEvent, IAttributeChangedEvent
    {
        public AttributeChangedEvent(IEnumerable<IGeometry> items) :
            base(items)
        {
        }
    }
}
