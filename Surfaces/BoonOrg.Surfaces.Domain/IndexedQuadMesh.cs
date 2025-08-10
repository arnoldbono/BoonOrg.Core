// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;

namespace BoonOrg.Surfaces.Domain
{
    public sealed class IndexedQuadMesh : IndexedMeshBase<IndexedQuad>
    {
        public IndexedQuadMesh()
        {
            Vertices = new IVector[] { };
            Elements = new IndexedQuad[] { };
        }

        public IndexedQuadMesh(IVector[] vertices, IndexedQuad[] quads)
        {
            Vertices = vertices;
            Elements = quads;
        }

        public override int[] ElementIndices()
        {
            var indices = new int[Elements.Length * 4];

            int i = 0;
            foreach (var quad in Elements)
            {
                indices[i++] = quad.Index1;
                indices[i++] = quad.Index2;
                indices[i++] = quad.Index3;
                indices[i++] = quad.Index4;
            }

            return indices;
        }
    }
}
