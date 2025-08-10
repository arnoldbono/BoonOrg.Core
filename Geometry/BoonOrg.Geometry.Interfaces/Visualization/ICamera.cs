// (c) 2020, 2017 Roland Boon

using BoonOrg.Identification;
using System.ComponentModel;

namespace BoonOrg.Geometry.Visualization
{
    public interface ICamera : IIdentifiable, INotifyPropertyChanged
    {
        IPoint Location { get; set; }

        IVector LookDirection { get; set; }

        IPoint Target { get; set; }

        IVector UpDirection { get; set; }

        double ConeAngle { get; set; }

        double Near { get; set; }

        double Far { get; set; }
    }
}
