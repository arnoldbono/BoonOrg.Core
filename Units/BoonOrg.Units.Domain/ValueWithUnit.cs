// (c) 2015, Roland Boon

namespace BoonOrg.Units
{
    public class ValueWithUnit : IValueWithUnit
    {
        private readonly IUnitSystem m_unitSystem;

        public double Value { get; private set; }

        public IUnit Unit { get; private set; }

        public string ValueText
        {
            get
            {
                return m_unitSystem.GetActiveValue(Unit, Value).ToString(@"N2");
            }
            set
            {
                Parse(value);
            }
        }

        public string UnitText => m_unitSystem.GetText(Unit);

        public double AdjustResultForUnit(double @value, IUnit unit)
        {
            return unit.ToSI(@value);
        }

        public string ConvertUsingUnit(double @value, IUnit unit)
        {
            double v = @value;
            if (!m_unitSystem.UseSI && !unit.IsSI)
            {
                v = unit.ToSI(v);
            }
            return v.ToString(@"N2");
        }

        public ValueWithUnit(double @value, IUnit unit, IUnitSystem unitSystem)
        {
            m_unitSystem = unitSystem;

            System.Diagnostics.Debug.Assert(unit.IsSI);

            Unit = unit;
            Value = Unit.ToSI(@value);
        }

        public bool Parse(string text)
        {
            if (double.TryParse(text, out double result))
            {
                Value = m_unitSystem.FromActiveValue(Unit, result);
                return true;
            }
            return false;
        }

        public static implicit operator double(ValueWithUnit v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ValueText;
        }
    }
}
