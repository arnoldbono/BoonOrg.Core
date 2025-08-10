// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry
{
    public sealed class RenderDataCreator : IRenderDataCreator
    {
        public IRenderData Create(float[] vertices, float[] propertyValues, float[] normals, int[] indices)
        {
            return new RenderData(vertices, propertyValues, normals, indices);
        }
    }
}
