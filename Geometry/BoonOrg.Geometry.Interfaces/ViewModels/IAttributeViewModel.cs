// (c) 2019, 2023 Roland Boon

using System;
using System.ComponentModel;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.ViewModels
{
    public interface IAttributeViewModel : INotifyPropertyChanged
    {
        Guid Id { get; set; }

        bool Visible { get; set; }

        IGeometryAttributeContainer Attributes { get; set; }

        void UpdateViewModel();
    }
}
