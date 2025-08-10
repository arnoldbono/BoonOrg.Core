// (c) 2017, 2019 Roland Boon

using System;

namespace BoonOrg.Geometry.Logic
{
    public interface IGeometryDeleter
    {
        void Delete(IGeometry geometry);

        void Delete(Guid geometryId);

        void Delete(string geometryName);
    }
}
