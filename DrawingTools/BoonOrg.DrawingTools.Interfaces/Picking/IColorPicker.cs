// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.DrawingTools.Picking
{
    public interface IColorPicker
    {
        void Begin();

        void End();

        IColor Register(IGeometry geometry);

        IGeometry Find(byte red, byte green, byte blue);

        IGeometry Find(IColor color);
    }
}
