// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Events
{
    public interface IGeometryEventFactory
    {
        T Create<T>(IEnumerable<IGeometry> items) where T : class, IGeometryEvent;
    }
}
