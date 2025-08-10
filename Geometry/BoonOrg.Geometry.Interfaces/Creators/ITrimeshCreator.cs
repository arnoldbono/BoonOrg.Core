// (c) 2017, 2023 Roland Boon

using System.Collections.Generic;

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface ITrimeshCreator : ICreator<ITrimesh>
    {
        void AddTriangle(int index1, int index2, int index3, bool reverseOrientation);

        void AddTriangle(int index1, int index2, int index3);

        int AddVertex(IPoint vertex);

        int AddVertex(IPoint vertex, IVector normal);

        void AddTrimesh(ITrimesh trimesh);

        ITrimesh Create(IEnumerable<ITriangle> triangles, string name);

        ITrimesh Create(IEnumerable<int> triangleVertexIndices, IEnumerable<IPoint> triangleVertices, string name);

        ITrimesh Create(IEnumerable<int> triangleVertexIndices, IEnumerable<IPoint> triangleVertices, IEnumerable<IVector> normals, string name);

        ITrimesh Create(string name);

        void UpdateIndexCheckForDuplicates();

        void Clear();
    }
}
