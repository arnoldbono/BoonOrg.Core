// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry;

using BoonOrg.DrawingTools.Textures;

namespace BoonOrg.DrawingTools
{
    public interface ITrimeshPropertyTextureCoordinatesService
    {
        public const string TextureCoordinatesPropertyName = @"uv";

        public void AddTextureCoordinates(ITrimesh trimesh, IEnumerable<ITextureCoordinate> textureCoordinates);

        IEnumerable<ITextureCoordinate> GetTextureCoordinates(ITrimesh trimesh);

        ITextureCoordinate GetTextureCoordinates(ITrimesh trimesh, int index);
    }
}
