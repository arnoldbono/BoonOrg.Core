// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces
{
    public interface IIndexedMesh<T> where T : class
    {
        IVector[] Vertices { get; set; }
        
        IVector[] Normals { get; set; }

        T[] Elements { get; set; }

        int[] ElementIndices();

        float[] PropertyArray(IPropertyAttribute property);

        float[] VerticesArray3();

        float[] NormalsArray3();

        IPropertyAttribute[] Properties { get; set; }
    }
}
