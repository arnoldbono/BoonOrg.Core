// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Surfaces;

namespace BoonOrg.DrawingTools.Rendering
{
    internal sealed class RepresentationFactory : IRepresentationFactory
    {
        private readonly IConvertToIndexedTriangleMesh m_convertToIndexedTriangleMesh;
        private readonly Func<ISurfaceRepresentation> m_geometryRepresentationFunc;

        public RepresentationFactory(IConvertToIndexedTriangleMesh convertToIndexedTriangleMesh,
            Func<ISurfaceRepresentation> geometryRepresentationFunc)
        {
            m_convertToIndexedTriangleMesh = convertToIndexedTriangleMesh;
            m_geometryRepresentationFunc = geometryRepresentationFunc;
        }

        public ISurfaceRepresentation GetOrCreate(IGeometry geometry, IGeometryAttributeContainer attributes)
        {
            var representation = attributes.GetAttribute<ISurfaceRepresentation>(geometry);
            if (representation != null)
            {
                return representation;
            }

            var drawSurfaceAttribute = attributes.GetAttribute<IDrawSurfaceAttribute>(geometry);

            var indexedTriangleMesh = m_convertToIndexedTriangleMesh.Convert(geometry, drawSurfaceAttribute.FacetedTriangles);
            if (indexedTriangleMesh == null)
            {
                return null;
            }

            representation = m_geometryRepresentationFunc();
            representation.Mesh = indexedTriangleMesh;

            attributes.AddAttribute(geometry, representation);

            return representation;
        }

    }
}
