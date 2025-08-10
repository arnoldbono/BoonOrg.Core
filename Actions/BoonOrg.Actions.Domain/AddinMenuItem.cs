// (c) 2017 Roland Boon

using System;

namespace BoonOrg.Actions.Domain
{
    internal sealed class AddinMenuItem : IAddinMenuItem
    {
        private Action m_action;

        public string Path { get; set; }

        public bool CanCheck { get; set; }

        public bool Checked { get; set; }

        public bool Enabled { get; set; }

        public AddinMenuItem()
        {
        }

        public AddinMenuItem(string path, Action action)
        {
            Path = path;
            m_action = action;
        }

        public void Execute()
        {
            m_action?.Invoke();
        }

        public void SetAction(Action action)
        {
            m_action = action;
        }
    }
}
