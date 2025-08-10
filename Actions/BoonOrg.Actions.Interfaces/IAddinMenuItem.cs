// (c) 2017, 2019 Roland Boon

using System;

namespace BoonOrg.Actions
{
    public interface IAddinMenuItem : IAddinCommand
    {
        string Path { get; set; }

        bool CanCheck { get; set; }

        bool Checked { get; set;  }

        bool Enabled { get; set; }

        void SetAction(Action action);
    }
}
