// (c) 2023 Roland Boon

using System;
using System.Diagnostics;
using System.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Png;

using BoonOrg.DrawingTools.Textures;

namespace BoonOrg.DrawingTools.Services
{
    public sealed class TextureService : ITextureService
    {
        private readonly Func<IBitmapTexture> m_textureFunc;

        public TextureService(Func<IBitmapTexture> textureFunc)
        {
            m_textureFunc = textureFunc;
        }

        public void Save(IBitmapTexture texture)
        {
            Debug.Assert(texture.Image != null);

            texture.Image.Save(texture.Path, new PngEncoder { CompressionLevel = PngCompressionLevel.BestSpeed });
        }

        public IBitmapTexture Read(string path, string name)
        {
            var texture = m_textureFunc();
            texture.Identification.Rename(name);

            if (!File.Exists(path))
            {
                return texture;
            }

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                texture.Image = Image.Load<Rgba32>(fileStream);
            }

            return texture;
        }
    }
}
