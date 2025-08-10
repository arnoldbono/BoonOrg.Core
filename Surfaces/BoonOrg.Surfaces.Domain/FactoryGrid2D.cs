// (c) 2023 Roland Boon

using System;
using System.Threading;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Surfaces.Domain
{
    public sealed class FactoryGrid2D : IFactoryGrid2D
    {
        private readonly IVectorCreator m_vectorCreator;

        private Thread m_thread;
        private IGrid2D m_grid;

        public FactoryGrid2D(IVectorCreator vectorCreator)
        {
            m_vectorCreator = vectorCreator;
        }

        public IGrid2D PopGrid()
        {
            var grid = m_grid;
            if (grid != null)
            {
                m_grid = null;
            }
            return grid;
        }

        public void BeginCreate(int vertexCountSide1, int vertexCountSide2)
        {
            m_thread = new Thread(_ => Create(vertexCountSide1, vertexCountSide2))
            {
                Name = @"FactoryGrid2D"
            };
            m_thread.Start();
        }

        public void Create(int vertexCountSide1, int vertexCountSide2)
        {
            m_grid = new Grid2D(m_vectorCreator.Create(), m_vectorCreator.Create(0.0, 0.0, 1.0), 2.0, 2.0, vertexCountSide1, vertexCountSide2);
            m_thread = null;
        }
    }
}
 