// (c) 2017, 2023 Roland Boon

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Visualization;
using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain.Visualization
{
    internal sealed class AmbientLight : GeometryItem, IAmbientLight
    {
        public IColor Color { get; set; }

        public AmbientLight(IIdentity identity, IColorCreator colorCreator) : base(identity)
        {
            Color = colorCreator.Create();
        }
    }
}
