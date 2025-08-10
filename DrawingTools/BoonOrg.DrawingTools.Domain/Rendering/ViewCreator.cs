// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.Rendering
{
    public sealed class ViewCreator : IViewCreator
    {
        private readonly Func<IView> m_viewFunc;

        public ViewCreator(Func<IView> viewFunc)
        {
            m_viewFunc = viewFunc;
        }

        public IView Create(float width, float height)
        {
            var view = m_viewFunc();
            view.Initialize(width, height);
            return view;
        }
    }
}
