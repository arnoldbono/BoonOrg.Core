// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

using BoonOrg.Identification;
using BoonOrg.Identification.Domain;

namespace BoonOrg.DrawingTools.Rendering
{
    public sealed class ViewContainer : IdentifiableCollection<IView>, IViewContainer, IIdentifiable
    {
        private readonly Subject<IView> m_activeViewSubject = new();
        private readonly List<IView> m_views = new();

        private int m_activeView = int.MinValue;

        public IObservable<IView> ActiveViewChanged => m_activeViewSubject;

        public IView[] Views => m_views.ToArray();

        public IView ActiveView => m_activeView == int.MinValue ? null : m_views[m_activeView];

        public ViewContainer(IIdentity identity) : base(identity)
        {
        }

        public void SetActiveView(IView view)
        {
            var activeView = m_activeView;

            if (view == null)
            {
                m_activeView = int.MinValue;
            }
            else
            {
                if (!m_views.Contains(view))
                {
                    m_views.Add(view);
                }

                m_activeView = m_views.IndexOf(view);
            }

            if (m_activeView != activeView)
            {
                m_activeViewSubject.OnNext(view);
            }
        }
    }
}
