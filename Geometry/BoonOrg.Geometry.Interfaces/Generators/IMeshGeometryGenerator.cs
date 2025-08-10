// (c) 2017, 2022, 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Generators
{
    public interface IMeshGeometryGenerator<T> : IMeshGeometryGenerator where T : IGeometry
    {
        IEnumerable<IMeshGeometry> Generate(T @object);
    }

    public interface IMeshGeometryGenerator
    {
        IEnumerable<IMeshGeometry> Generate(IGeometry geometry);

        bool HandlesType(Type type);
    }
}
