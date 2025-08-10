// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry
{
    public class RenderData : IRenderData
    {
        public float[] Vertices { get; }

        public float[] PropertyValues { get; }

        public float[] Normals { get; }

        public int[] Indices { get; }

        public RenderData(float[] vertices, float[] propertyValues, float[] normals, int[] indices)
        {
            Vertices = vertices;
            PropertyValues = propertyValues;
            Normals = normals;
            Indices = indices;
        }
    }
}
