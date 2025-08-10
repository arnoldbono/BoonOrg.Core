// (c) 2022 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class MeshGeometry : IMeshGeometry
    {
        private readonly List<IPoint> m_positions = new();
        private readonly List<IVector> m_normals = new();
        private readonly List<int> m_triangleIndices = new();
        private readonly List<IPoint> m_textureCoordinates = new();

        public IEnumerable<IPoint> Positions => m_positions;

        public IEnumerable<IVector> Normals => m_normals;

        public IEnumerable<int> TriangleIndices => m_triangleIndices;

        public int PositionCount => m_positions.Count;

        public int NormalCount => m_normals.Count;

        public int TriangleCount => m_triangleIndices.Count / 3;

        public IEnumerable<IPoint> TextureCoordinates => m_textureCoordinates;

        public void ClearTriangleIndices() => m_triangleIndices.Clear();

        public void AddNormal(IVector normal) => m_normals.Add(normal);

        public void AddPosition(IPoint position) => m_positions.Add(position);

        public void AddTriangleIndex(int triangleIndex) => m_triangleIndices.Add(triangleIndex);

        public void AddTextureCoordinates(IPoint textureCoordinates) => m_textureCoordinates.Add(textureCoordinates);

    }
}
