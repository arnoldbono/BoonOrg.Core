// (c) 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.DrawingTools.ColorMapping;

namespace BoonOrg.DrawingTools.Textures
{
    public interface IBitmapTextureSkyHook
    {
        void LoadTextureFile(Stream stream, string textureName);

        void UpdateBitmapTexture();

        IBitmapTexture BitmapTexture { get; }

        IColorSet[] ColorSets { get; }
    }
}
