// (c) 2015, Roland Boon

using System;

namespace BoonOrg.Units
{
    /// <summary>
    /// A list of units. The list can be modified later and/or read from file.
    /// </summary>
    public interface IUnitSystem
    {
        event EventHandler SIChanged;

        IUnit GetUnitSI(UnitCategory category);

        IUnit GetUnitUS(UnitCategory category);

        bool UseSI { get; set; }

        string GetText(IUnit unit);

        IUnit GetActiveUnit(IUnit unit);

        double GetActiveValue(IUnit unit, double val);

        double FromActiveValue(IUnit unit, double val);
    }
}
