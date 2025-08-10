// (c) 2017, 2022, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Generators;

namespace BoonOrg.Geometry.Domain.Generators
{
    internal sealed class TrimeshToMeshGeometryGenerator : IMeshGeometryGenerator<ITriangleContainer>
    {
        private readonly IBasicMeshGeometryGenerator m_meshGeometryGenerator;

        public TrimeshToMeshGeometryGenerator(IBasicMeshGeometryGenerator meshGeometryGenerator)
        {
            m_meshGeometryGenerator = meshGeometryGenerator;
        }

        public IEnumerable<IMeshGeometry> Generate(IGeometry geometry)
        {
            return Generate((ITriangleContainer)geometry);
        }

        public IEnumerable<IMeshGeometry> Generate(ITriangleContainer trimesh)
        {
            m_meshGeometryGenerator.Reset();

            m_meshGeometryGenerator.AddTriangles(trimesh, false);

            var box = trimesh.GetBoundingBox();
            m_meshGeometryGenerator.AddTextureCoordinates(box.GetArea());

            return new List<IMeshGeometry> { m_meshGeometryGenerator.Mesh };
        }

        public bool HandlesType(Type type)
        {
            return typeof(ITriangleContainer).IsAssignableFrom(type);
        }
    }
}
