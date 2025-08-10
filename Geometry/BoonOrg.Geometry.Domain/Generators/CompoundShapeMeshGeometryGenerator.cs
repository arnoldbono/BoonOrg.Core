// (c) 2017, 2022 Roland Boon

using System;
using System.Collections.Generic;
using BoonOrg.Geometry.Generators;

namespace BoonOrg.Geometry.Domain.Generators
{
    internal sealed class CompoundShapeMeshGeometryGenerator : IMeshGeometryGenerator<ICompoundShape<ITriangleContainer>>
    {
        private readonly IBasicMeshGeometryGenerator m_meshGeometryGenerator;

        public CompoundShapeMeshGeometryGenerator(IBasicMeshGeometryGenerator meshGeometryGenerator)
        {
            m_meshGeometryGenerator = meshGeometryGenerator;
        }

        public IEnumerable<IMeshGeometry> Generate(IGeometry geometry)
        {
            return Generate((ICompoundShape<ITriangleContainer>)geometry);
        }

        public IEnumerable<IMeshGeometry> Generate(ICompoundShape<ITriangleContainer> shape)
        {
            m_meshGeometryGenerator.Reset();

            foreach (var container in shape.Containers)
            {
                m_meshGeometryGenerator.AddTriangles(container, false);
            }

            IBoundingBox box = shape.GetBoundingBox();
            m_meshGeometryGenerator.AddTextureCoordinates(box.GetArea());

            return new List<IMeshGeometry> { m_meshGeometryGenerator.Mesh };
        }

        public bool HandlesType(Type type)
        {
            return typeof(ICompoundShape<ITriangleContainer>).IsAssignableFrom(type);
        }
    }
}
