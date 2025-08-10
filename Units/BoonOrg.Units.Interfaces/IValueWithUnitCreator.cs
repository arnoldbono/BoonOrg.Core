// (c) 2015, Roland Boon

namespace BoonOrg.Units
{
    public interface IValueWithUnitCreator
    {
        IValueWithUnit Create(double value, UnitCategory category);
    }
}
