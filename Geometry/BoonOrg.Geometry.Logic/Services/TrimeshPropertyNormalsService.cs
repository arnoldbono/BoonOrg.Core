// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Geometry.Domain;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic.Services
{
    internal sealed class TrimeshPropertyNormalsService : ITrimeshPropertyNormalsService
    {
        private static IProperty<IVector> GetPropertyNormals(ITrimesh trimesh)
        {
            return trimesh.PropertyContainer.GetProperty<IProperty<IVector>>(ITrimeshPropertyNormalsService.NormalsPropertyName);
        }

        public void AddNormals(ITrimesh trimesh, IEnumerable<IVector> normals)
        {
            var normalsProperty = GetPropertyNormals(trimesh);
            if (normalsProperty == null)
            {
                normalsProperty = new Property<IVector> { Name = ITrimeshPropertyNormalsService.NormalsPropertyName };
                trimesh.PropertyContainer.AddProperty(normalsProperty);
            }

            normalsProperty.AddRange(normals);
        }

        public IEnumerable<IVector> GetNormals(ITrimesh trimesh) => GetPropertyNormals(trimesh)?.Values;

        public IVector GetNormal(ITrimesh trimesh, int index)
        {
            var normalsProperty = GetPropertyNormals(trimesh);
            return normalsProperty != null && index < normalsProperty.Count ? normalsProperty[index] : null;
        }

    }
}
