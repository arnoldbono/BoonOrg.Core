// (c) 2019 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    public interface IGeometryEvent
    {
        IEnumerable<IGeometry> Items { get; }
    }
}
