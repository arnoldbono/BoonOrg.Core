// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Services
{
    public interface ITrimeshPropertyNormalsService
    {
        public const string NormalsPropertyName = @"normals";

        public void AddNormals(ITrimesh trimesh, IEnumerable<IVector> normals);

        IEnumerable<IVector> GetNormals(ITrimesh trimesh);

        IVector GetNormal(ITrimesh trimesh, int index);
    }
}
