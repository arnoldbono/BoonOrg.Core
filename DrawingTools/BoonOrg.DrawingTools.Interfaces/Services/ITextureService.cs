// (c) 2023, 2024 Roland Boon

using System;

using BoonOrg.DrawingTools.Textures;

namespace BoonOrg.DrawingTools.Services
{
    public interface ITextureService
    {
        void Save(IBitmapTexture texture);

        IBitmapTexture Read(string path, string name);
    }
}
