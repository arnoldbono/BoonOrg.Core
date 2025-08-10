// (c) 2017 Roland Boon

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Visualization;
using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain.Visualization
{
    internal sealed class DirectionalLight : GeometryItem, IDirectionalLight
    {
        public IColor Color { get; set; }

        public IVector Direction { get; set; }

        public DirectionalLight(IIdentity identity, IColorCreator colorCreator, IVectorCreator vectorCreator) : base(identity)
        {
            Color = colorCreator.Create();
            Direction = vectorCreator.Create(-1.0, -1.0, -1.0);
        }
    }
}
