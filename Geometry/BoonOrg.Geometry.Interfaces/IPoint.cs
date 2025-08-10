// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IPoint
    {
        double X { get; set; }

        double Y { get; set; }

        double Z { get; set; }

        double this[int i] { get; set; }

        double Length2 { get; }

        double Length { get; }

        void Set(IPoint point);

        void Set(double x, double y, double z);

        void SetAt(int i, double d);

        double GetAt(int i);

        void Scale(double scale);

        void Zero();

        void Assign(double value);

        bool IsSame(IPoint point);

        bool IsSame(IPoint point, double accuracy);

        IPoint Clone();
    }
}
