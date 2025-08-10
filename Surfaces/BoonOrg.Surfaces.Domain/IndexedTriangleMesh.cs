// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;

namespace BoonOrg.Surfaces.Domain
{
    public class IndexedTriangleMesh : IndexedMeshBase<IndexedTriangle>
    {
        public IndexedTriangleMesh()
        {
            Vertices = Array.Empty<IVector>();
            Normals = Array.Empty<IVector>();
            Elements = Array.Empty<IndexedTriangle>();
        }

        public IndexedTriangleMesh(IVector[] vertices, IndexedTriangle[] triangles)
        {
            Vertices = vertices;
            Normals = Array.Empty<IVector>();
            Elements = triangles;
        }

        public IndexedTriangleMesh(IVector[] vertices, IVector[] normals, IndexedTriangle[] triangles)
        {
            Vertices = vertices;
            Normals = normals;
            Elements = triangles;
        }

        public override int[] ElementIndices()
        {
            var indices = new int[Elements.Length * 3];

            int i = 0;
            foreach (var triangle in Elements)
            {
                indices[i++] = triangle.Index1;
                indices[i++] = triangle.Index2;
                indices[i++] = triangle.Index3;
            }

            return indices;
        }
    }
}
