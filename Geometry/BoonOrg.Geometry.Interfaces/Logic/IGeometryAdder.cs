// (c) 2019 Roland Boon

using System.Collections.Generic;

using BoonOrg.Storage;

namespace BoonOrg.Geometry.Logic
{
    public interface IGeometryAdder
    {
        void Add(IGeometry geometry);

        void Add(IGeometry geometry, IDocument document);

        void Add(IEnumerable<IGeometry> geometry);

        void Add(IEnumerable<IGeometry> geometry, IDocument document);
    }
}
