// (c) 2023, 2024 Roland Boon

using System;

namespace BoonOrg.Geometry.Persistence
{
    public interface ITrimeshExporterFactory
    {
        ITrimeshExporter Create(string selectedExporter);
    }
}
