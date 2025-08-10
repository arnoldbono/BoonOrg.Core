// (c) 2020, 2017 Roland Boon

using System.ComponentModel;

using System.Runtime.CompilerServices;
using BoonOrg.Identification;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Domain.Visualization
{
    public class Camera : GeometryItem, ICamera
    {
        private IPoint m_location;
        private IVector m_lookDirection;
        private IPoint m_target;
        private IVector m_upDirection;
        private double m_coneAngle;
        private double m_near;
        private double m_far;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged([CallerMemberName] string propertyName = @"")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IPoint Location
        {
            get { return m_location; }
            set
            {
                if (value == null)
                {
                    return;
                }

                m_location.Set(value);
                NotifyChanged();
            }
        }

        public IVector LookDirection
        {
            get { return m_lookDirection; }
            set
            {
                if (value == null)
                {
                    return;
                }

                m_lookDirection.Set(value);
                NotifyChanged();
            }
        }

        public IPoint Target
        {
            get { return m_target; }
            set
            {
                if (value == null)
                {
                    return;
                }

                m_target.Set(value);
                NotifyChanged();
            }
        }

        public IVector UpDirection
        {
            get { return m_upDirection; }
            set
            {
                if (value == null)
                {
                    return;
                }

                m_upDirection.Set(value);
                NotifyChanged();
            }
        }

        public double ConeAngle
        {
            get => m_coneAngle;
            set
            {
                if (value == m_coneAngle)
                {
                    return;
                }

                m_coneAngle = value;
                NotifyChanged();
            }
        }

        public double Near
        {
            get => m_near;
            set
            {
                if (value == m_near)
                {
                    return;
                }

                m_near = value;
                NotifyChanged();
            }
        }

        public double Far
        {
            get => m_far;
            set
            {
                if (value == m_far)
                {
                    return;
                }

                m_far = value;
                NotifyChanged();
            }
        }

        public Camera(IIdentity identity) : base(identity)
        {
            m_location = new Point(1.0, 1.0, 1.0);
            m_lookDirection = new Vector(-1.0, -1.0, -1.0);
            m_upDirection = new Vector(0.0, 0.0, 1.0);
            m_target = new Point(0.0, 0.0, 0.0);
            ConeAngle = 30;
        }
    }
}
