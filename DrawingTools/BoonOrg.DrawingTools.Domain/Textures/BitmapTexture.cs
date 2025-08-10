// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using BoonOrg.Identification;
using BoonOrg.Identification.Domain;

namespace BoonOrg.DrawingTools.Textures
{
    public sealed class BitmapTexture : IBitmapTexture
    {
        public IIdentity Identification { get; } = new Identity();

        public Image<Rgba32> Image { get; set; }

        public string Path { get; set; }

        public byte[] Load()
        {
            if (!File.Exists(Path))
            {
                return null;
            }

            var data = new List<byte>();
            var buffer = new byte[1024];

            using (var fileStream = File.OpenRead(Path))
            {
                while (true)
                {
                    var count = fileStream.Read(buffer);
                    if (count == buffer.Length)
                    {
                        data.AddRange(buffer);
                    }
                    else
                    {
                        for (int i = 0; i < count; ++i)
                        {
                            data.Add(buffer[i]);
                        }

                        break;
                    }
                }
            }

            return data.ToArray();
        }

        public float[] GetColors(int colorSetIndex)
        {
            if (colorSetIndex < 0)
            {
                return Array.Empty<float>();
            }

            return GetColors(Image, colorSetIndex);
        }


        public int GetColorSetCount()
        {
            return Image.Height;
        }


        private static float[] GetColors(Image<Rgba32> image, int row)
        {
            var height = image.Height;
            var width = image.Width;

            var bytes = new byte[width * height * 3];
                image.ProcessPixelRows(accessor =>
            {
                int i = 0;
                for (int y = 0; y < accessor.Height; y++)
                {
                    Span<Rgba32> pixelRow = accessor.GetRowSpan(y);

                    // pixelRow.Length has the same value as accessor.Width,
                    // but using pixelRow.Length allows the JIT to optimize away bounds checks:
                    for (int x = 0; x < pixelRow.Length; x++)
                    {
                        ref Rgba32 pixel = ref pixelRow[x];
                        bytes[i++] = pixel.B;
                        bytes[i++] = pixel.G;
                        bytes[i++] = pixel.R;
                    }
                }
            });


            var size = width * 4;
            var data = new float[size];

            if (row >= height)
            {
                Array.Clear(data);
                return data;
            }

            int offset = row * width * 3;
            var j = 0;
            for (int i = 0; i < width; ++i)
            {
                data[j++] = bytes[offset++] / 255.0f;
                data[j++] = bytes[offset++] / 255.0f;
                data[j++] = bytes[offset++] / 255.0f;
                data[j++] = 1.0f;
            }

            return data;
        }

    }
}
