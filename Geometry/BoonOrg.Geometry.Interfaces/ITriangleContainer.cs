// (c) 2017, 2023 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public interface ITriangleContainer : ISurface
    {
        int TriangleCount { get; }

        IEnumerable<ITriangle> Triangles { get; }

        IEnumerable<int> TriangleVertexIndices { get; }

        IEnumerable<IPoint> Vertices { get; }

        IEnumerable<IVector> Normals { get; }

        void Add(IEnumerable<ITriangle> triangles);

        void Add(IEnumerable<int> triangleVertexIndices, IEnumerable<IPoint> vertices);

        void Add(IEnumerable<IVector> normals);

        ITriangle GetTriangle(int index);

        IPoint GetTriangleVertex(int index);

        void AddTriangle(IPoint vertex1, IPoint vertex2, IPoint vertex3);

        void AddTriangles(IEnumerable<ITriangle> triangles);

        void AddTriangle(ITriangle triangle);

        void Clear();

        // double ComputeArea();
    }
}
