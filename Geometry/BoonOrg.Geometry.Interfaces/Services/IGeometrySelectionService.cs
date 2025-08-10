// (c) 2019, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Services
{
    public interface IGeometrySelectionService
    {
        IEnumerable<IGeometry> Selection { get; }

        bool AddSelection(IEnumerable<IGeometry> items);

        bool AddSelection(IGeometry item);

        void SetSelection(IEnumerable<IGeometry> items);

        void SetSelection(IGeometry item);

        void ClearSelection();

        IEnumerable<IIdentity> GetSelectionPath(IGeometry geometry);
    }
}
