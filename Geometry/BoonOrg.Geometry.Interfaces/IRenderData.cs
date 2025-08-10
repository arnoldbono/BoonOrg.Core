// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Geometry
{
    public interface IRenderData
    { 
        float[] Vertices { get; }

        float[] PropertyValues { get; }

        float[] Normals { get; }

        int[] Indices { get; }
    }
}
