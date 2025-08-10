// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    public interface ILayerGridToTrimeshTransformer
    {
        ITrimesh Transform(ILayerGrid surface, string name);

        /// <summary>
        ///  If at least one above the plane, rotate nodes until the first node is the peak.
        /// </summary>
        List<IPoint> GetPeakOnFirstNode(IPoint[] vertices, int aboveCount);

        void RotateNodes(List<IPoint> nodes);
    }
}
