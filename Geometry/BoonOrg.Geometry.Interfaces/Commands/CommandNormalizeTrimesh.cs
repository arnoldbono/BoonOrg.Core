// (c) 2019 Roland Boon

using BoonOrg.Commands;

namespace BoonOrg.Geometry.Commands
{
    public sealed class CommandNormalizeTrimesh : ICommand
    {
        public string Surface { get; set; }
    }
}
