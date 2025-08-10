// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using BoonOrg.Geometry.Generators;

namespace BoonOrg.Geometry.Domain.Generators
{
    internal sealed class InstancedShapeToMeshGeometryGenerator : IMeshGeometryGenerator<IInstancedShape>
    {
        private readonly IBasicMeshGeometryGenerator m_meshGeometryGenerator;

        public InstancedShapeToMeshGeometryGenerator(IBasicMeshGeometryGenerator meshGeometryGenerator)
        {
            m_meshGeometryGenerator = meshGeometryGenerator;
        }

        public IEnumerable<IMeshGeometry> Generate(IGeometry geometry)
        {
            return Generate((IInstancedShape)geometry);
        }

        public IEnumerable<IMeshGeometry> Generate(IInstancedShape instancedShape)
        {
            m_meshGeometryGenerator.Reset();

            var trimesh = instancedShape.Trimesh;
            m_meshGeometryGenerator.AddTriangles(trimesh, false);

            instancedShape.Add(trimesh.TriangleVertexIndices, trimesh.Vertices);
            var box = trimesh.GetBoundingBox();
            // ROBOCOP: Use the instanced matrices to get the real bounding box
            m_meshGeometryGenerator.AddTextureCoordinates(box.GetArea());

            return new List<IMeshGeometry> { m_meshGeometryGenerator.Mesh };
        }

        public bool HandlesType(Type type)
        {
            return typeof(IInstancedShape).IsAssignableFrom(type);
        }
    }
}
