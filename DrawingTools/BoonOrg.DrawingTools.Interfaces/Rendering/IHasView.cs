// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.Rendering
{
    public interface IHasView
    {
        IView View { get; }
    }
}
