// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry;

using BoonOrg.DrawingTools.Textures;

namespace BoonOrg.DrawingTools
{
    public sealed class TrimeshPropertyTextureCoordinatesService : ITrimeshPropertyTextureCoordinatesService
    {
        private readonly IPropertyCreator m_propertyCreator;

        public TrimeshPropertyTextureCoordinatesService(IPropertyCreator propertyCreator)
        {
            m_propertyCreator = propertyCreator;
        }

        private static IProperty<ITextureCoordinate> GetPropertyTextureCoordinates(ITrimesh trimesh)
        {
            return trimesh.PropertyContainer.GetProperty<IProperty<ITextureCoordinate>>(ITrimeshPropertyTextureCoordinatesService.TextureCoordinatesPropertyName);
        }

        public void AddTextureCoordinates(ITrimesh trimesh, IEnumerable<ITextureCoordinate> textureCoordinates)
        {
            var textureCoordinatesProperty = GetPropertyTextureCoordinates(trimesh);
            if (textureCoordinatesProperty == null)
            {
                textureCoordinatesProperty = m_propertyCreator.Create<ITextureCoordinate>(ITrimeshPropertyTextureCoordinatesService.TextureCoordinatesPropertyName);
                trimesh.PropertyContainer.AddProperty(textureCoordinatesProperty);
            }

            textureCoordinatesProperty.AddRange(textureCoordinates);
        }

        public IEnumerable<ITextureCoordinate> GetTextureCoordinates(ITrimesh trimesh) => GetPropertyTextureCoordinates(trimesh)?.Values;

        public ITextureCoordinate GetTextureCoordinates(ITrimesh trimesh, int index)
        {
            var textureCoordinatesProperty = GetPropertyTextureCoordinates(trimesh);
            return textureCoordinatesProperty != null && index < textureCoordinatesProperty.Count ? textureCoordinatesProperty[index] : null;
        }

    }
}
