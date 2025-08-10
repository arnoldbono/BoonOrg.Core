// (c) 2017, 2023 Roland Boon

using System;

namespace BoonOrg.Geometry
{
    /// <summary>
    /// A mesh of triangles.
    /// </summary>
    public interface ITrimesh : ITriangleContainer
    {
        /// <summary>
        /// Reference to a material (color, reflection, et cetera)
        /// </summary>
        string Material { get; set; }

        /// <summary>
        /// Reference to a texture, for U,V mapping a property
        /// </summary>
        string Texture { get; set; }

        IPropertyContainer PropertyContainer { get; }

        /// <summary>
        /// Returns the minimum <paramref name="min"/> and maximum <paramref name="max"/> of the triangle indices.
        /// </summary>
        /// <param name="min">The minimum index found.</param>
        /// <param name="max">The maximum index found.</param>
        void GetIndicesRange(out int min, out int max);
    }
}
