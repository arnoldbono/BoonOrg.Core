// (c) 2023 Roland Boon

namespace BoonOrg.DrawingTools.Textures
{
    public interface ITextureCoordinate
    {
        double U { get; set; }

        double V { get; set; }

        void Set(ITextureCoordinate textureCoordinate);

        void Set(double u, double v);

        bool IsSame(ITextureCoordinate textureCoordinate);

        bool IsSame(ITextureCoordinate textureCoordinate, double accuracy);

        ITextureCoordinate Clone();
    }
}
