// (c) 2023 Roland Boon

using System;
using BoonOrg.Commands;

namespace BoonOrg.Surfaces.Commands
{
    public sealed class CommandToggleFacetedTriangles : ICommand
    {
        public Guid Id { get; set; }
    }
}
