// (c) 2023 Roland Boon

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Surfaces;

namespace BoonOrg.DrawingTools.Rendering
{
    public interface IRepresentationFactory
    {
        ISurfaceRepresentation GetOrCreate(IGeometry geometry, IGeometryAttributeContainer attributes);
    }
}