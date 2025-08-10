// (c) 2019 Roland Boon

using System;

using BoonOrg.Storage;

namespace BoonOrg.Geometry.Events
{
    internal sealed class GeometryDocumentCloseEvent : GeometryEvent, IGeometryDocumentCloseEvent
    {
        public GeometryDocumentCloseEvent() : base(Array.Empty<IGeometry>())
        {
        }
    }
}
