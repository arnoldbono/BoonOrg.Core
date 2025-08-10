// (c) 2024 Roland Boon

using System;

namespace BoonOrg.Surfaces.Persistence
{
    public interface IGrid2DExporterFactory
    {
        IGrid2DExporter Create(string selectedExporter);
    }
}
