// (c) 2023 Roland Boon

using System;

namespace BoonOrg.Surfaces.Persistence
{
    public sealed class Grid2DData
    {
        public double LengthSide1 { get; set; }
        
        public double LengthSide2 { get; set; }

        public int VertexCountSide1 { get; set; }
        
        public int VertexCountSide2 { get; set; }
        
        public double[] Center { get; set; }
        
        public double[] Normal { get; set; }
    }
}
