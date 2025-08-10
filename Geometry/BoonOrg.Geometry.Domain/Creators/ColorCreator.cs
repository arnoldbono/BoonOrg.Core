// (c) 2018, 2023 Roland Boon

using System;

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class ColorCreator : IColorCreator
    {
        private readonly Func<IColor> m_colorFunc;

        public ColorCreator(Func<IColor> colorFunc)
        {
            m_colorFunc = colorFunc;
        }

        public IColor Create() => Create(byte.MaxValue);

        public IColor Create(byte red, byte green, byte blue) => Create(red, green, blue, byte.MaxValue);

        public IColor Create(double red, double green, double blue) => Create(D2B(red), D2B(green), D2B(blue), byte.MaxValue);

        public IColor Create(byte red, byte green, byte blue, byte alpha)
        {
            var color = m_colorFunc();
            color.Set(red, green, blue, alpha);
            return color;
        }

        public IColor Create(double red, double green, double blue, double alpha) => Create(D2B(red), D2B(green), D2B(blue), D2B(alpha));

        public IColor Create(byte brightness) => Create(brightness, brightness, brightness, byte.MaxValue);

        public IColor Create(double brightness) => Create(D2B(brightness));

        private static byte D2B(double color)
        {
            return (color < 0.0) ? byte.MinValue : (color > 1.0) ? byte.MaxValue : (byte)(color * byte.MaxValue);
        }
    }
}
