// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IBoundingBoxProvider
    {
        IBoundingBox GetBoundingBox();

        void ExpandBoundingBox(IBoundingBox box);
    }
}
