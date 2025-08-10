// (c) 2023 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Visualization
{
    public interface IMaterial : IIdentifiable
    {
        IPbrProperty PbrProperty { get; set; }
    }
}
