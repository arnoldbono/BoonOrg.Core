// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;

namespace BoonOrg.Surfaces
{
    public interface IConvertToIndexedTriangleMesh
    {
        /// <summary>
        /// Convert the geometry into an indexed, triangular mesh.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="facetedTriangles">If true, the vertices of the triangle each get the same normal.</param>
        /// <returns>An indexed, triangular  mesh that represents the geometry.</returns>
        IIndexedMesh<IndexedTriangle> Convert(IGeometry geometry, bool facetedTriangles);
    }
}
