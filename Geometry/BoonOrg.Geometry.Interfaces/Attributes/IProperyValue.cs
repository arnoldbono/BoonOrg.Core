// (c) 2024 Roland Boon

namespace BoonOrg.Geometry.Attributes
{
    public interface IPropertyValue
    {
        void ResetToMaxValue();

        void ResetToMinValue();

        /// <summary>
        /// Update MinValue and MaxValue based on this value.
        /// </summary>
        void Update(IPropertyValue minValue, IPropertyValue maxValue);

        /// <summary>
        /// Normalize the value based on MinValue and MaxValue.
        /// </summary>
        void Normalize(IPropertyValue minValue, IPropertyValue maxValue);
    }
}
