// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry
{
    public enum RelationToPlane
    {
        Above,
        Below,
        Intersecting
    };

    public interface IPlane : ISurface
    {
        IPoint Center { get; }

        IVector Normal { get; }

        double Distance { get; }

        double Width { get; set; }

        double Length { get; set; }

        IEnumerable<IPoint> Vertices { get; }

        void Set(IPoint center, IVector normal, string name);

        double GetDistance(IPoint coordinate);

        double GetTranslatedInnerProductWithNormal(IPoint coordinate);

        double GetInnerProductWithNormal(IPoint coordinate);

        RelationToPlane GetRelation(IPoint coordinate);
    }
}
