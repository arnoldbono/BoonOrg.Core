// (c) 2023 Roland Boon

using System;

using BoonOrg.Identification;
using BoonOrg.Storage;

namespace BoonOrg.DrawingTools.Rendering
{
    internal class ViewContainerCreator : IInitialDocumentContentCreator
    {
        private readonly Func<IViewContainer> m_viewContainerFunc;

        public ViewContainerCreator(Func<IViewContainer> viewContainerFunc)
        {
            m_viewContainerFunc = viewContainerFunc;
        }

        public IIdentifiable Create() => m_viewContainerFunc();
    }
}
