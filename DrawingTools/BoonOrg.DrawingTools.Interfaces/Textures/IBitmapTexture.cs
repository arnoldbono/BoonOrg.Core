// (c) 2023 Roland Boon

using System;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using BoonOrg.Identification;

namespace BoonOrg.DrawingTools.Textures
{
    public interface IBitmapTexture : IIdentifiable
    {
        Image<Rgba32> Image { get; set; }

        string Path { get; set; }

        byte[] Load();

        float[] GetColors(int colorSetIndex);

        int GetColorSetCount();
    }
}
