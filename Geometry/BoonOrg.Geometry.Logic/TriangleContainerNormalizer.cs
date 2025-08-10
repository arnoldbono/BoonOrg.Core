// (c) 2019 Roland Boon

using System.Linq;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class TriangleContainerNormalizer : ITriangleContainerNormalizer
    {
        private readonly IVectorCreator m_vectorCreator;
        private readonly IVectorLogic m_vectorLogic;

        public TriangleContainerNormalizer(IVectorCreator vectorCreator,
            IVectorLogic vectorLogic)
        {
            m_vectorCreator = vectorCreator;
            m_vectorLogic = vectorLogic;
        }

        public bool Normalize(ITriangleContainer surface)
        {
            var indices = surface.TriangleVertexIndices.ToList();
            var vertices = surface.Vertices.ToList();

            var direction1 = m_vectorCreator.Create(0.0, 0.0, 1.0);
            var direction2 = m_vectorCreator.Create();

            var normalizedIndices = new int[indices.Count];

            normalizedIndices[0] = indices[0];
            normalizedIndices[1] = indices[1];
            normalizedIndices[2] = indices[2];

            var normalized = false;

            var count = indices.Count;
            for (int i = 0; i < count; i += 3)
            {
                var vertex1 = vertices[indices[i]];
                var vertex2 = vertices[indices[i + 1]];
                var vertex3 = vertices[indices[i + 2]];

                ComputeNormalDirection(vertex1, vertex2, vertex3, direction2);
                if (m_vectorLogic.GetInnerProduct(direction1, direction2) < 0)
                {
                    normalizedIndices[i] = indices[i + 2];
                    normalizedIndices[i + 1] = indices[i + 1];
                    normalizedIndices[i + 2] = indices[i];

                    normalized = true;
                }
                else
                {
                    normalizedIndices[i] = indices[i];
                    normalizedIndices[i + 1] = indices[i + 1];
                    normalizedIndices[i + 2] = indices[i + 2];
                }
            }

            if (!normalized)
            {
                return false;
            }

            surface.Clear();
            surface.Add(normalizedIndices, vertices);
            return true;
        }

        private static void ComputeNormalDirection(IPoint vertex1, IPoint vertex2, IPoint vertex3, IVector vector)
        {
            double dx1 = vertex2.X - vertex1.X;
            double dy1 = vertex2.Y - vertex1.Y;
            double dz1 = vertex2.Z - vertex1.Z;

            double dx2 = vertex3.X - vertex1.X;
            double dy2 = vertex3.Y - vertex1.Y;
            double dz2 = vertex3.Z - vertex1.Z;

            vector.X = dy1 * dz2 - dz1 * dy2;
            vector.Y = dz1 * dx2 - dx1 * dz2;
            vector.Z = dx1 * dy2 - dy1 * dx2;
        }
    }
}
