// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.Rendering
{
    public interface IViewCreator
    {
        IView Create(float width, float height);
    }
}
