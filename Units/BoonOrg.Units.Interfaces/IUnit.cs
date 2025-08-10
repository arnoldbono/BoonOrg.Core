// (c) 2015, Roland Boon

namespace BoonOrg.Units
{
    /// <summary>
    /// A unit of a certain unit category with a fallback to its corresponding SI unit (if applicable).
    /// </summary>
    public interface IUnit
    {
        string FullName { get; }

        string Name { get; }

        ICategory Category { get; }

        double Conversion { get; }

        IUnit SI { get; }

        bool IsSI { get; }


        double ToSI(double @value);

        double ToUnit(double @value);
    }
}
