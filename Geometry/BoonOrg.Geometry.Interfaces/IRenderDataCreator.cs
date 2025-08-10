// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry
{
    public interface IRenderDataCreator
    {
        IRenderData Create(float[] vertices, float[] propertyValues, float[] normals, int[] indices);
    }
}
