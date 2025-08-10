// (c) 2015, Roland Boon

namespace BoonOrg.Units
{
    internal sealed class ValueWithUnitCreator : IValueWithUnitCreator
    {
        private IUnitSystem m_unitSystem;

        public ValueWithUnitCreator(IUnitSystem unitSystem)
        {
            m_unitSystem = unitSystem;
        }

        public IValueWithUnit Create(double @value, UnitCategory category)
        {
            return new ValueWithUnit(@value, m_unitSystem.GetUnitSI(category), m_unitSystem);
        }
    }
}
