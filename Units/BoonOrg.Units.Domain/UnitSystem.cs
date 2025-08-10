// (c) 2015, Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

namespace BoonOrg.Units
{
    internal sealed class UnitSystem : IUnitSystem
    {
        public static readonly Unit kUnitMeter = new Unit("meter", "m", UnitCategory.Length);
        public static readonly Unit kUnitFeet = new Unit("feet", "feet", UnitCategory.Length, 0.3048, kUnitMeter);
        public static readonly Unit kUnitCubicMeter = new Unit("cubic meters", "m^3", UnitCategory.Volume);
        public static readonly Unit kUnitCubicFeet = new Unit("cubic feet", "feet^3", UnitCategory.Volume, 0.3048 * 0.3048 * 0.3048, kUnitCubicMeter);
        public static readonly Unit kUnitBarrel = new Unit("US barrel", "US bbl", UnitCategory.Volume, 0.1589873, kUnitMeter);

        public event EventHandler SIChanged;

        public List<Unit> Units { get; private set; }

        private bool m_useSI = true;

        public bool UseSI
        {
            get
            {
                return m_useSI;
            }
            set
            {
                if (m_useSI != value)
                {
                    m_useSI = value;
                    SIChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public UnitSystem()
        {
            Units = new List<Unit> { kUnitMeter, kUnitFeet, kUnitCubicMeter, kUnitCubicFeet, kUnitBarrel };
        }

        public IUnit GetUnitSI(UnitCategory category)
        {
            switch (category)
            {
                case UnitCategory.Length: return kUnitMeter;
                case UnitCategory.Volume: return kUnitCubicMeter;
            }
            return null;
        }

        public IUnit GetUnitUS(UnitCategory category)
        {
            switch (category)
            {
                case UnitCategory.Length: return kUnitFeet;
                case UnitCategory.Volume: return kUnitCubicFeet;
                case UnitCategory.Barrel: return kUnitBarrel;
            }
            return null;
        }

        public string GetText(IUnit unit)
        {
            string rv = "-";
            if (unit != null)
            {
                rv = GetActiveUnit(unit).ToString();
            }
            return rv;
        }

        public IUnit GetActiveUnit(IUnit unit)
        {
            if (!unit.IsSI && UseSI)
            {
                unit = unit.SI;
            }
            else if (unit.IsSI && !UseSI)
            {
                unit = Units.First(u => !u.IsSI && u.SI.FullName == unit.FullName);
            }
            return unit;
        }

        public double GetActiveValue(IUnit unit, double val)
        {
            double rv = val;
            if (unit != null)
            {
                if (!unit.IsSI)
                {
                    rv = unit.ToSI(val);
                }
                if (!UseSI)
                {
                    rv = GetActiveUnit(unit).ToUnit(val);
                }
            }
            return rv;
        }

        public double FromActiveValue(IUnit unit, double val)
        {
            double rv = val;
            if (unit != null)
            {
                if (!unit.IsSI)
                {
                    rv = unit.ToSI(val);
                }
                if (!UseSI)
                {
                    rv = GetActiveUnit(unit).ToSI(val);
                }
            }
            return rv;
        }

        // TODO. Make persistant
        // TODO. Allow editing of the list.
    }
}
