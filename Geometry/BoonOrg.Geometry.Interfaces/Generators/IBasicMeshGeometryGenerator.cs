// (c) 2023 Roland Boon

namespace BoonOrg.Geometry.Generators
{
    public interface IBasicMeshGeometryGenerator : IMeshGeometryGenerator
    {
        IMeshGeometry Mesh { get; }

        int AddNodeGetIndex(IPoint node);

        int AddOrReuseNodeGetIndex(IPoint node);

        void AddTextureCoordinates(IArea area);

        void AddTriangle(int index1, int index2, int index3);

        void AddTriangle(ITriangle triangle);

        void AddTriangles(ITriangleContainer container, bool facetedTriangles);

        void ClearTriangleIndices();

        void Reset();
    }
}