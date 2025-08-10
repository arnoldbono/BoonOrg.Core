// (c) 2019, 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Identification;

using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.Services
{
    internal sealed class GeometrySelectionService : IGeometrySelectionService
    {
        private readonly IGeometryEventService m_geometryEventService;

        private List<IGeometry> m_selection = new();

        public IEnumerable<IGeometry> Selection => m_selection;

        public GeometrySelectionService(IGeometryEventService geometryEventService)
        {
            m_geometryEventService = geometryEventService;
        }

        public bool AddSelection(IEnumerable<IGeometry> items)
        {
            bool added = false;
            foreach (var item in items)
            {
                if (!m_selection.Contains(item))
                {
                    m_selection.Add(item);
                    added = true;
                }
            }

            if (!added)
            {
                return false;
            }

            InformSelectionChanged();
            return true;
        }

        public bool AddSelection(IGeometry item)
        {
            if (m_selection.Contains(item))
            {
                return false;
            }

            m_selection.Add(item);
            InformSelectionChanged();
            return true;
        }

        public void SetSelection(IEnumerable<IGeometry> items)
        {
            bool change = m_selection.Any();
            m_selection.Clear();
            if (!AddSelection(items) && change)
            {
                InformSelectionChanged();
            }
        }

        public void SetSelection(IGeometry item)
        {
            if (m_selection.Count == 1 && m_selection.Contains(item))
            {
                return;
            }

            // Reset and only select the new item.
            m_selection.Clear();
            AddSelection(item);
        }

        public void ClearSelection()
        {
            m_selection.Clear();
            InformSelectionChanged();
        }

        private void InformSelectionChanged()
        {
            m_geometryEventService.Selected(m_selection.ToArray());
        }

        public IEnumerable<IIdentity> GetSelectionPath(IGeometry geometry)
        {
            var path = new List<IIdentity>();
            var identity = geometry?.Identification;
            while (identity != null)
            {
                path.Add(identity);
                identity = identity.Parent;
            }
            path.Reverse();
            return path;
        }
    }
}
