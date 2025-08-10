// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public abstract  class IndexedMeshBase<T> : IIndexedMesh<T> where T : class 
    {
        public IVector[] Vertices { get; set; }

        public IVector[] Normals { get; set; }

        public T[] Elements { get; set; }

        public abstract int[] ElementIndices();

        public float[] PropertyArray(IPropertyAttribute property)
        {
            var random = new Random((int)DateTime.Now.Ticks);

            var count = Vertices.Length;
            var propertyValues = new float[count];

            if (property is not IIndexedPropertyAttribute<IPropertyValueDouble> propertyDouble)
            {
                int i = 0;
                foreach (var vertex in Vertices)
                {
                    propertyValues[i++] = (float)random.NextDouble();
                }
            }
            else
            {
                for (var j = 0; j < count; ++j)
                {
                    propertyValues[j] = (float)propertyDouble[j].Value;
                }
            }

            return propertyValues;
        }

        public float[] VerticesArray3()
        {
            var vertices = new float[Vertices.Length * 3];

            var j = 0;
            foreach (var vertex in Vertices)
            {
                vertices[j++] = (float)vertex.X;
                vertices[j++] = (float)vertex.Y;
                vertices[j++] = (float)vertex.Z;
            }

            return vertices;
        }

        public float[] NormalsArray3()
        {
            var normals = new float[Normals.Length * 3];

            var j = 0;
            foreach (var normal in Normals)
            {
                normals[j++] = (float)normal.X;
                normals[j++] = (float)normal.Y;
                normals[j++] = (float)normal.Z;
            }

            return normals;
        }

        public IPropertyAttribute[] Properties { get; set; }
    }
}
