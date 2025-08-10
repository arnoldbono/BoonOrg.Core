// (c) 2023 Roland Boon

using BoonOrg.Geometry.Visualization;
using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain.Visualization
{
    internal sealed class Material : GeometryItem, IMaterial
    {
        public IPbrProperty PbrProperty { get; set; } = new PbrProperty();

        public Material(IIdentity identity) : base(identity)
        {
        }
    }
}
