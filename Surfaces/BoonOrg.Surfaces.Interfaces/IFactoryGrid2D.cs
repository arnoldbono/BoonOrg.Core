using System;

namespace BoonOrg.Surfaces
{
    public interface IFactoryGrid2D
    {
        IGrid2D PopGrid();

        void BeginCreate(int vertexCountSide1, int vertexCountSide2);

        void Create(int vertexCountSide1, int vertexCountSide2);
    }
}
 