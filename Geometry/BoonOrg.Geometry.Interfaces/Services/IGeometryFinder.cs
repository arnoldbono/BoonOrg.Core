// (c) 2019 Roland Boon

using System;

using BoonOrg.Storage;

namespace BoonOrg.Geometry.Services
{
    public interface IGeometryFinder
    {
        IGeometry Find(string geometryName);

        IGeometry Find(string geometryName, IDocument document);

        IGeometry Find(Guid geometryId);

        IGeometry Find(Guid geometryId, IDocument document);

        string FindUniqueName(string name);

        string FindUniqueName(string name, IDocument document);
    }
}
