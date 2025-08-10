// (c) 2017 Roland Boon

using BoonOrg.Commands;

namespace BoonOrg.Geometry.Commands
{
    public sealed class CommandConvertLayerGridToTrimesh : ICommand
    {
        public string SourceSurface { get; set; }
        public string TargetSurface { get; set; }
    }
}
