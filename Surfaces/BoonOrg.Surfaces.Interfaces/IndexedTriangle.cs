using System;

namespace BoonOrg.Surfaces
{
    /// <summary>
    /// A triangle with indices pointing to vertices in a vertex array.
    /// </summary>
    public class IndexedTriangle
    {
        public IndexedTriangle(int index1, int index2, int index3)
        {
            Index1 = index1;
            Index2 = index2;
            Index3 = index3;
        }

        public int Index1 { get; }
        
        public int Index2 { get; }

        public int Index3 { get; }
    }
}
