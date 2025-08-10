// (c) 2023 Roland Boon

using System;
using BoonOrg.Geometry.Creators;
using BoonOrg.Surfaces.Domain;

namespace BoonOrg.Surfaces.Persistence
{
    public sealed class Grid2DDataConverter
    {
        private readonly IVectorCreator m_vectorCreator;

        public Grid2DDataConverter(IVectorCreator vectorCreator)
        {
            m_vectorCreator = vectorCreator;
        }

        public IGrid2D FromData(Grid2DData data)
        {
            if (data.Center == null || data.Normal == null ||
                data.LengthSide1 <= 0.0 || data.LengthSide2 <= 0.0 ||
                data.VertexCountSide1 < 2 || data.VertexCountSide2 < 2)
            {
                return null;
            }

            var center = m_vectorCreator.Create(data.Center[0], data.Center[1], data.Center[2]);
            var normal = m_vectorCreator.Create(data.Normal[0], data.Normal[1], data.Normal[2]);

            return new Grid2D(center, normal, data.LengthSide1, data.LengthSide2, data.VertexCountSide1, data.VertexCountSide2);
        }

        public Grid2DData ToData(IGrid2D grid)
        {
            var center = grid.Center;
            var normal = grid.Normal;

            var data = new Grid2DData
            {
                LengthSide1 = grid.LengthSide1,
                LengthSide2 = grid.LengthSide2,
                VertexCountSide1 = grid.VertexCountSide1,
                VertexCountSide2 = grid.VertexCountSide2,
                Center = new double[3] { center.X, center.Y, center.Z },
                Normal = new double[3] { normal.X, normal.Y, normal.Z }
            };

            return data;
        }
    }
}
