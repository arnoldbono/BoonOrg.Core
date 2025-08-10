// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Visualization;
using BoonOrg.Geometry;
using BoonOrg.Geometry.Creators;

namespace BoonOrg.DrawingTools.Picking
{
    internal sealed class ColorPicker : IColorPicker
    {
        private readonly IColorCreator m_colorCreator;

        private Dictionary<UInt32, IGeometry> m_registrations = new ();

        public ColorPicker(IColorCreator colorCreator)
        {
            m_colorCreator = colorCreator;
        }

        public void Begin()
        {
            m_registrations.Clear();
        }

        public void End()
        {
            m_registrations.Clear();
        }

        public IColor Register(IGeometry geometry)
        {
            var color = GetRGB(m_registrations.Count);
            var rgb = (UInt32)(color.Red << 16) | (UInt32)(color.Green << 8) | (UInt32)color.Blue;
            m_registrations[rgb] = geometry;
            return color;
        }

        public IGeometry Find(byte red, byte green, byte blue)
        {
            var rgb = (UInt32)(red << 16) | (UInt32)(green << 8) | (UInt32)blue;
            return Find(rgb);
        }

        public IGeometry Find(IColor color)
        {
            var rgb = (UInt32)(color.Red << 16) | (UInt32)(color.Green << 8) | (UInt32)color.Blue;
            return Find(rgb);
        }

        public IGeometry Find(UInt32 rgb)
        {
            return m_registrations.ContainsKey(rgb) ? m_registrations[rgb] : null;
        }

        // https://stackoverflow.com/questions/309149/generate-distinctly-different-rgb-colors-in-graphs
        private IColor GetRGB(int index)
        {
            int[] p = GetPattern(index);
            return m_colorCreator.Create((byte)GetElement(p[0]), (byte)GetElement(p[1]), (byte)GetElement(p[2]), (byte)255);
        }

        private static int GetElement(int index)
        {
            int value = index - 1;
            int v = 0;
            for (int i = 0; i < 8; i++)
            {
                v |= (value & 1);
                v <<= 1;
                value >>= 1;
            }
            v >>= 1;
            return v & 0xFF;
        }

        private static int[] GetPattern(int index)
        {
            int n = (int)Math.Cbrt(index);
            index -= (n * n * n);
            int[] p = new int[3] { n, n, n };
            if (index == 0)
            {
                return p;
            }
            index--;
            int v = index % 3;
            index /= 3;
            if (index < n)
            {
                p[v] = index % n;
                return p;
            }
            index -= n;
            p[v] = index / n;
            p[++v % 3] = index % n;
            return p;
        }
    }
}
