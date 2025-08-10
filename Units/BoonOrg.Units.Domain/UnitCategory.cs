// (c) 2015, Roland Boon

namespace BoonOrg.Units
{
    /// <summary>
    /// Wrapper for <code>UnitCategory</code> to check validity of value.
    /// Semi useful in case unit system is read form a file.
    /// </summary>
    internal sealed class Category : ICategory
    {
        private UnitCategory m_category;

        public Category(UnitCategory category)
        {
            this.m_category = category;
            PostConditionAfterContruction();
        }

        public static explicit operator Category(int category)
        {
            Category rv = new Category((UnitCategory)category);
            return rv;
        }

        public static implicit operator Category(UnitCategory category)
        {
            Category rv = new Category(category);
            return rv;
        }

        public static implicit operator UnitCategory(Category c)
        {
            System.Diagnostics.Debug.Assert(c != null);
            return c.m_category;
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void PostConditionAfterContruction()
        {
            bool rv = (int)m_category >= (int)UnitCategory.Length && (int)m_category <= (int)UnitCategory.Volume;
            System.Diagnostics.Debug.Assert(rv);
        }
    }
}
