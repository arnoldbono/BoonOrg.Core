// (c) 2015, Roland Boon

namespace BoonOrg.Units
{
    public interface IValueWithUnit
    {
        double Value { get; }

        IUnit Unit { get; }

        string ValueText { get; set; }

        string UnitText { get; }

        double AdjustResultForUnit(double @value, IUnit unit);

        string ConvertUsingUnit(double @value, IUnit unit);

        bool Parse(string text);
    }
}
