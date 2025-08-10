// (c) 2023 Roland Boon

using System;

using BoonOrg.DrawingTools.Textures;

namespace BoonOrg.DrawingTools.ColorMapping
{
    public interface IColorSetService
    {
        float[] GetData(IColorSet colorSet, int numberOfFractions, bool interpolated);

        /// <summary>
        /// Makes sure the color set fractions start at 0 and end at 1 and are monotonously increasing.
        /// </summary>
        /// <param name="colorSet"></param>
        void FilterEntries(IColorSet colorSet);

        IBitmapTexture Create(IColorSet[] colorSets, string textureName, int numberOfFractions, bool interpolated);
    }
}
