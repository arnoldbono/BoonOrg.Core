// (c) 2024 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public sealed class PropertyValueTextureMap : IPropertyValueTextureMap
    {
        public double U { get; set; } = 0.0;

        public double V { get; set; } = 0.0;

        public PropertyValueTextureMap()
        {
        }

        public PropertyValueTextureMap(double u, double v)
        {
            U = u;
            V = v;
        }

        public void ResetToMaxValue()
        {
            U = double.MaxValue;
            V = double.MaxValue;
        }

        public void ResetToMinValue()
        {
            U = double.MinValue;
            V = double.MinValue;
        }

        public void Update(IPropertyValue minValue, IPropertyValue maxValue)
        {
            var MinValue = (PropertyValueTextureMap)minValue;
            var MaxValue = (PropertyValueTextureMap)maxValue;

            if (MinValue.U > U)
            {
                MinValue.U = U;
            }

            if (MinValue.V > V)
            {
                MinValue.V = V;
            }

            if (MaxValue.U < U)
            {
                MaxValue.U = U;
            }

            if (MaxValue.V < V)
            {
                MaxValue.V = V;
            }
        }

        public void Normalize(IPropertyValue minValue, IPropertyValue maxValue)
        {
            var MinValue = (PropertyValueTextureMap)minValue;
            var MaxValue = (PropertyValueTextureMap)maxValue;
            var divider = MaxValue.U - MinValue.U;
            U = divider == 0.0 ? 1.0 : (U - MinValue.U) / divider;
            divider = MaxValue.V - MinValue.V;
            V = divider == 0.0 ? 1.0 : (V - MinValue.V) / divider;
        }

        public override string ToString() { return $@"{U}, {V}"; }
    }
}
