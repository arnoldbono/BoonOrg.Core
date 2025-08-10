// (c) 2019, 2023 Roland Boon

using System;

namespace BoonOrg.Geometry.Events
{
    public interface IGeometryModifiedEvent : IGeometryEvent
    {
        bool UpdateVertices { get; set; }

        bool UpdatePropertyValues { get; set; }

        bool UpdateColorMap { get; set; }

        bool AttributeChange { get; set; }
    }
}
