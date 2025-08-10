// (c) 2017 Roland Boon

using BoonOrg.Commands;

namespace BoonOrg.Geometry.Commands
{
    public sealed class CommandDisplaceSurface : ICommand
    {
        public string SurfaceName { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double OffsetZ { get; set; }
    }
}
