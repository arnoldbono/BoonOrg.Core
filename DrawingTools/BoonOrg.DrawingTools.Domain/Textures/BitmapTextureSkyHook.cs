// (c) 2023 Roland Boon

using System;
using System.IO;
using System.Linq;

using BoonOrg.DrawingTools.ColorMapping;
using BoonOrg.DrawingTools.Persistence;

namespace BoonOrg.DrawingTools.Textures
{
    public sealed class BitmapTextureSkyHook : IBitmapTextureSkyHook
    {
        private readonly IColorSetService m_colorSetService;
        private readonly IColorSetXmlReader m_colorSetXmlReader;

        public BitmapTextureSkyHook(IColorSetService colorSetService, IColorSetXmlReader colorSetXmlReader)
        {
            m_colorSetService = colorSetService;
            m_colorSetXmlReader = colorSetXmlReader;
        }

        public void LoadTextureFile(Stream stream, string textureName)
        {
            TextureName = textureName;

            ColorSets = m_colorSetXmlReader.Read(stream).ToArray();
            UpdateBitmapTexture();
        }

        public void UpdateBitmapTexture()
        {
            BitmapTexture = m_colorSetService.Create(ColorSets, TextureName, 2048, true);
        }

        public string TextureName { get; private set; }

        public IBitmapTexture BitmapTexture { get; private set; }

        public IColorSet[] ColorSets { get; private set; }

    }
}
