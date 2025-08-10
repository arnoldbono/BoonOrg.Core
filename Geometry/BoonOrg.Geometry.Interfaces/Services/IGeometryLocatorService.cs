// (c) 2019 Roland Boon

using System;

using BoonOrg.Storage;

namespace BoonOrg.Geometry.Services
{
    public interface IGeometryLocatorService
    {
        T Find<T>(string name, IDocument document = null) where T : class, IGeometry;

        T Find<T>(Guid id, IDocument document = null) where T : class, IGeometry;
    }
}
