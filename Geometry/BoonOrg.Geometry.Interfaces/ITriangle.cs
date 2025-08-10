// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public interface ITriangle : IBoundingBoxProvider
    {
        IPoint Vertex1 { get; set; }

        IPoint Vertex2 { get; set; }

        IPoint Vertex3 { get; set; }

        IEnumerable<IPoint> Vertices { get; }

        void Assign(IPoint c1, IPoint c2, IPoint c3);

        void Assign(IReadOnlyList<IPoint> coordinates);

        //double ComputeArea();

        IVector ComputeNormal();

        void Flip();

        void Translate(IVector vector);
    }
}
