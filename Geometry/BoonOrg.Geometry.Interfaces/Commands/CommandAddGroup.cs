// (c) 2019 Roland Boon

using BoonOrg.Commands;

namespace BoonOrg.Geometry.Commands
{
    public sealed class CommandAddGroup : ICommand
    {
        public string Name { get; set; }

        /// <summary>
        /// The hierarchy, names, preceded and separated by single forward slashes.
        /// </summary>
        public string Path { get; set; }
    }
}
