// (c) 2017, 2023 Roland Boon

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Visualization;
using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain.Visualization
{
    internal sealed class SpotLight : GeometryItem, ISpotLight
    {
        public IColor Color { get; set; }

        public IPoint Location { get; set; }

        public IVector Direction { get; set; }

        public double InnerCone { get; set; }

        public double OuterCone { get; set; }

        public SpotLight(IIdentity identity, IColorCreator colorCreator, IVectorCreator vectorCreator) : base(identity)
        {
            Color = colorCreator.Create();
            Location = vectorCreator.Create(1.0, 1.0, 1.0);
            Direction = vectorCreator.Create(-1.0, -1.0, -1.0);
            InnerCone = 30;
            OuterCone = 60;
        }
    }
}
