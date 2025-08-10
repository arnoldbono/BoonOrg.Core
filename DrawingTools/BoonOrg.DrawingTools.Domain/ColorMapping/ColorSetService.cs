// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using BoonOrg.DrawingTools.Textures;
using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.DrawingTools.ColorMapping
{
    /// <summary>
    /// Converts a ColorSet to a float array containing vec4 colors.
    /// </summary>
    public sealed class ColorSetService : IColorSetService
    {
        private readonly Func<IBitmapTexture> m_textureFunc;
        private readonly Func<IColorPin> m_colorDefFunc;
        private readonly IColorCreator m_colorCreator;

        public ColorSetService(Func<IColorPin> colorDefFunc, Func<IBitmapTexture> textureFunc, IColorCreator colorCreator)
        {
            m_colorDefFunc = colorDefFunc;
            m_textureFunc = textureFunc;
            m_colorCreator = colorCreator;
        }

        public float[] GetData(IColorSet colorSet, int numberOfFractions, bool interpolated)
        {
            return interpolated ? GetInterpolatedData(colorSet, numberOfFractions) : GetData(colorSet, numberOfFractions);
        }

        public void FilterEntries(IColorSet colorSet)
        {
            var entries = new List<IColorPin>(colorSet.Pins);

            entries.Sort(new ColorPinByFractionComparer());

            foreach (var entry in entries)
            {
                if (entry.Fraction < 0.0)
                {
                    entry.Fraction = 0.0;
                }
                else if (entry.Fraction > 1.0)
                {
                    entry.Fraction = 1.0;
                }
            }

            colorSet.PaddedFront = false;
            colorSet.PaddedBack = false;

            var empty = !entries.Any();

            if (empty || entries[0].Fraction != 0.0)
            {
                var colorDef = m_colorDefFunc();
                colorDef.Fraction = 0.0;
                colorDef.Color = empty ? m_colorCreator.Create(0, 0, 0) : entries[0].Color;
                entries.Insert(0, colorDef);
                colorSet.PaddedFront = true;
            }

            if (empty || entries[^1].Fraction < 1.0)
            {
                var colorDef = m_colorDefFunc();
                colorDef.Fraction = 1.0;
                colorDef.Color = empty ? m_colorCreator.Create(255, 255, 255) : entries[^1].Color;
                entries.Add(colorDef);
                colorSet.PaddedBack = true;
            }

            colorSet.Pins = entries.ToArray();
        }

        public IBitmapTexture Create(IColorSet[] colorSets, string textureName, int numberOfFractions, bool interpolated)
        {
            var texturePath = Path.ChangeExtension(textureName, ".png");

            var height = colorSets.Length;
            var width = numberOfFractions;

            var imageData = new byte[width * height * 4];
            var k = 0;
            for (int i = 0; i < height; ++i)
            {
                var colors = GetData(colorSets[i], numberOfFractions, true);

                var l = 0;
                for (int j = 0; j < numberOfFractions; ++j)
                {
                    imageData[k++] = (byte)(colors[l++] * 255.0);
                    imageData[k++] = (byte)(colors[l++] * 255.0);
                    imageData[k++] = (byte)(colors[l++] * 255.0);
                    imageData[k++] = (byte)255; // Alpha channel is always opaque
                    ++l;
                }
            }

            using var ms = new MemoryStream(imageData);
            using Image<Rgba32> image = Image.Load<Rgba32>(ms);            

            var texture = m_textureFunc();
            texture.Identification.Rename(textureName);
            texture.Path = texturePath;
            texture.Image = image;

            return texture;
        }

        private static float[] GetInterpolatedData(IColorSet colorSet, int numberOfFractions)
        {
            var colors = new List<float>();
            var pins = colorSet.Pins;

            var count = pins.Length;
            var first = pins[0];
            var second = pins[1];
            var j = 1;

            AddColor(colors, first.Color);

            for (int i = 1; i < numberOfFractions; ++i)
            {
                var fraction = i / (double)(numberOfFractions);
                while (second.Fraction < fraction)
                {
                    ++j;
                    if (j == count)
                    {
                        break;
                    }

                    first = second;
                    second = pins[j];
                }

                var f = (fraction - first.Fraction) / (second.Fraction - first.Fraction);

                AddColor(colors, first.Color, second.Color, 1.0 - f, f);
            }

            AddColor(colors, second.Color);

            return colors.ToArray();
        }

        private static float[] GetData(IColorSet colorSet, int numberOfFractions)
        {
            var colors = new List<float>();
            var entries = colorSet.Pins;

            int count = entries.Length;
            var first = entries[0];
            var second = entries[1];
            int j = 1;

            AddColor(colors, first.Color);

            for (int i = 1; i < numberOfFractions; ++i)
            {
                var fraction = i / (double)numberOfFractions;
                while (second.Fraction < fraction)
                {
                    ++j;
                    if (j == count)
                    {
                        break;
                    }

                    first = second;
                    second = entries[j];
                }

                AddColor(colors, first.Color);
            }

            AddColor(colors, second.Color);

            return colors.ToArray();
        }

        private static void AddColor(IList<float> colors, IColor color)
        {
            colors.Add(color.Red / 255.0f);
            colors.Add(color.Green / 255.0f);
            colors.Add(color.Blue / 255.0f);
            colors.Add(color.Alpha / 255.0f);
        }

        private static void AddColor(IList<float> colors, IColor color1, IColor color2, double f1, double f2)
        {
            colors.Add((float)(f1 * color1.Red + f2 * color2.Red) / 255.0f);
            colors.Add((float)(f1 * color1.Green + f2 * color2.Green) / 255.0f);
            colors.Add((float)(f1 * color1.Blue + f2 * color2.Blue) / 255.0f);
            colors.Add((float)(f1 * color1.Alpha + f2 * color2.Alpha) / 255.0f);
        }

        private sealed class ColorPinByFractionComparer : IComparer<IColorPin>
        {
            public int Compare(IColorPin x, IColorPin y)
            {
                if (object.ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (null == x)
                {
                    return -1;
                }

                if (null == y)
                {
                    return 1;
                }

                return x.Fraction < y.Fraction ? -1 : 1;
            }
        }
    }
}
