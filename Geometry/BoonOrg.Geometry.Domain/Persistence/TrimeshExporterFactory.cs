// (c) 2023, 2024 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

namespace BoonOrg.Geometry.Persistence
{
    internal sealed class TrimeshExporterFactory : ITrimeshExporterFactory
    {
        private readonly IEnumerable<ITrimeshExporter> m_exporters;

        public TrimeshExporterFactory(IEnumerable<ITrimeshExporter> exporters)
        {
            m_exporters = exporters;
        }

        public ITrimeshExporter Create(string selectedExporter)
        {
            return m_exporters.FirstOrDefault(x => string.Compare(x.Extension, $@".{selectedExporter}", true) == 0);
        }
    }
}
