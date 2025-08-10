// (c) 2024 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public sealed class PropertyValueDouble : IPropertyValueDouble
    {
        public double Value { get; set; } = 0.0;

        public PropertyValueDouble()
        {
        }

        public PropertyValueDouble(double value)
        {
            Value = value;
        }

        public void ResetToMaxValue()
        {
            Value = double.MaxValue;
        }

        public void ResetToMinValue()
        {
            Value = double.MinValue;
        }

        public void Update(IPropertyValue minValue, IPropertyValue maxValue)
        {
            var MinValue = (PropertyValueDouble)minValue;
            var MaxValue = (PropertyValueDouble)maxValue;

            if (MinValue.Value > Value)
            {
                MinValue.Value = Value;
            }

            if (MaxValue.Value < Value)
            {
                MaxValue.Value = Value;
            }
        }

        public void Normalize(IPropertyValue minValue, IPropertyValue maxValue)
        {
            var MinValue = (PropertyValueDouble)minValue;
            var MaxValue = (PropertyValueDouble)maxValue;
            var divider = MaxValue.Value - MinValue.Value;
            Value = divider == 0.0 ? 1.0 : (Value - MinValue.Value) / divider;
        }

        public override string ToString() { return Value.ToString(); }
    }
}
