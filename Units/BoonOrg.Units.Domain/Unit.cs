// (c) 2015, Roland Boon

namespace BoonOrg.Units
{
    /// <summary>
    /// A unit of a certain unit category with a fallback to its corresponding SI unit (if applicable).
    /// </summary>
    internal sealed class Unit : IUnit
    {
        public const double kConversionSI = 1.0;

        public string FullName { get; private set; }
        public string Name { get; private set; }
        public ICategory Category { get; private set; }
        public double Conversion { get; private set; }
        public IUnit SI { get; private set; }

        public bool IsSI
        {
            get
            {
                System.Diagnostics.Debug.Assert(Conversion != 0.0 && (SI == null || Conversion != kConversionSI));
                return Conversion == kConversionSI;
            }
        }

        public Unit()
        {
            FullName = "meter";
            Name = "m";
            Category = new Category(UnitCategory.Length);
            Conversion = kConversionSI;
            PostConditionAfterContruction();
            SI = null;
        }

        public Unit(string fullname, string name, Category category) : this(fullname, name, category, kConversionSI, null)
        {
        }

        public Unit(string fullname, string name, Category category, double conversion, Unit si)
        {
            FullName = fullname;
            Name = name;
            Category = category;
            Conversion = conversion;
            SI = si;
            PostConditionAfterContruction();
        }

        public double ToSI(double value)
        {
            double rv = value;
            if (!IsSI)
            {
                rv = value * Conversion;
            }
            return rv;
        }

        public double ToUnit(double value)
        {
            double rv = value;
            if (!IsSI)
            {
                rv = value / Conversion;
            }
            return rv;
        }

        public override string ToString()
        {
            return "(" + Name + ")";
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void PostConditionAfterContruction()
        {
            bool rv = !string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(Name);
            rv = rv && Category != null;
            rv = rv && Conversion != 0.0;
            rv = rv && SI == null || Conversion != kConversionSI;
            System.Diagnostics.Debug.Assert(rv);
        }
    }
}
