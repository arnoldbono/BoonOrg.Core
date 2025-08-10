// (c) 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Visualization
{
    public interface ILight : IIdentifiable
    {
        IColor Color { get; set; }
    }
}
