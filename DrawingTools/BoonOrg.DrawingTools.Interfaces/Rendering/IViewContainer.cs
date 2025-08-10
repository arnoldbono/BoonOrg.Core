// (c) 2023 Roland Boon

using System;

using BoonOrg.Identification;

namespace BoonOrg.DrawingTools.Rendering
{
    public interface IViewContainer : IIdentifiableCollection<IView>
    {
        IObservable<IView> ActiveViewChanged { get; }

        IView[] Views { get; }

        IView ActiveView { get; }

        void SetActiveView(IView view);
    }
}
