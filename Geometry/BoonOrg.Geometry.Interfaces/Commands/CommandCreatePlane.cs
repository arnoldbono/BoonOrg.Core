// (c) 2017 Roland Boon

using BoonOrg.Commands;

namespace BoonOrg.Geometry.Commands
{
    public sealed class CommandCreatePlane : ICommand
    {
        public string Name { get; set; }

        public IPoint Center { get; set; }

        public IVector Normal { get; set; }

        public double Width { get; set; }

        public double Length { get; set; }
    }
}
