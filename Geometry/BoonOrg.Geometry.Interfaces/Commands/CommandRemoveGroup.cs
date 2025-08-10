// (c) 2019 Roland Boon

using BoonOrg.Commands;

namespace BoonOrg.Geometry.Commands
{
    public sealed class CommandRemoveGroup : ICommand
    {
        public string PathIncludingName { get; set; }
    }
}
