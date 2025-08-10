// (c) 2017, 2023 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IGeometryOwner
    {
        bool ContainsGeometry(IGeometry geometry);

        void AddGeometry(IGeometry geometry);

        void AddOrReplaceGeometry(IGeometry geometry);

        void RemoveGeometry(IGeometry geometries);

        void RemoveGeometry(string geometryName);
    }
}
