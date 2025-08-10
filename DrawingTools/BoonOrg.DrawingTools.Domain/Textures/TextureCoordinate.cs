// (c) 2023 Roland Boon

using System;

namespace BoonOrg.DrawingTools.Textures
{
    public sealed class TextureCoordinate : ITextureCoordinate
    {
        public double U { get; set; }

        public double V { get; set; }

        public TextureCoordinate()
        {
        }

        public TextureCoordinate(double u, double v)
        {
            U = u;
            V = v;
        }

        public void Set(ITextureCoordinate textureCoordinate)
        {
            U = textureCoordinate.U;
            V = textureCoordinate.V;
        }

        public void Set(double u, double v)
        {
            U = u;
            V = v;
        }

        public bool IsSame(ITextureCoordinate textureCoordinate)
        {
            return textureCoordinate != null && textureCoordinate.U == U && textureCoordinate.V == V;
        }

        public bool IsSame(ITextureCoordinate textureCoordinate, double accuracy)
        {
            return textureCoordinate == null &&
                Math.Abs(textureCoordinate.U - U) < accuracy &&
                Math.Abs(textureCoordinate.V - V) < accuracy;
        }
        public ITextureCoordinate Clone()
        {
            return new TextureCoordinate(U, V);
        }

        public override string ToString()
        {
            return @$"[U: {U}, V: {V}]";
        }

    }
}
