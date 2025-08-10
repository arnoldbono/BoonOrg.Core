using System;

namespace BoonOrg.Surfaces
{
    /// <summary>
    /// A quad with indices pointing to vertices in a vertex array.
    /// </summary>
    public class IndexedQuad
    {
        public IndexedQuad(int index1, int index2, int index3, int index4)
        {
            Index1 = index1;
            Index2 = index2;
            Index3 = index3;
            Index3 = index4;
        }

        public int Index1 { get; }
        
        public int Index2 { get; }

        public int Index3 { get; }

        public int Index4 { get; }
    }
}
