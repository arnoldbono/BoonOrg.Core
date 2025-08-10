// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Identification;

namespace BoonOrg.Surfaces.Domain
{
    public class Grid2D : IGrid2D
    {
        public IVector Center { get; }

        public IVector Normal { get; }

        public double LengthSide1 { get; }

        public double LengthSide2 { get; }

        public int VertexCountSide1 { get; }

        public int VertexCountSide2 { get; }

        public IIdentity Identification => throw new NotImplementedException();

        //! A simple 2D Mesh at depth center.Z.
        public Grid2D(IVector center, IVector normal, double lengthSide1, double lengthSide2, int vertexCountSide1, int vertexCountSide2)
        {
            Center = center;
            Normal = normal;
            LengthSide1 = lengthSide1;
            LengthSide2 = lengthSide2;
            VertexCountSide1 = vertexCountSide1;
            VertexCountSide2 = vertexCountSide2;
        }

        public IBoundingBox GetBoundingBox()
        {
            throw new NotImplementedException();
        }

        public void ExpandBoundingBox(IBoundingBox box)
        {
            throw new NotImplementedException();
        }
    }
}
