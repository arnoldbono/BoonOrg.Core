// (c) 2024 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Domain.Attributes
{
    public class TextureMapAttribute : ITextureMapAttribute
    {
        public TextureMapAttribute()
        {
            Show = true;
            TextureMap = string.Empty;
        }

        public bool Show { get; set; }

        public string TextureMap { get; set; }
    }
}
