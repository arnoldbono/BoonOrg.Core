// (c) 2020 Roland Boon

using BoonOrg.Commands;

namespace BoonOrg.Geometry.Commands
{
    /// <summary>
    /// Create a circle.
    /// </summary>
    public sealed class CommandCreateCircle : ICommand
    {
        public string Name { get; set; }

        public IPoint Center { get; set; }

        public IVector Normal { get; set; }

        public double Radius { get; set; }

        public int Count { get; set; }
    }
}
