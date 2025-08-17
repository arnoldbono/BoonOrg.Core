// (c) 2015, 2017, 2019, 2023 Roland Boon

using System;
using System.Linq;

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Services;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class TrimeshCopier : ISurfaceCopy<ITrimesh>
    {
        private readonly ITrimeshCreator m_trimeshCreator;
        private readonly ITrimeshPropertyNormalsService m_normalsService;

        public TrimeshCopier(ITrimeshCreator trimeshCreator, ITrimeshPropertyNormalsService normalsService)
        {
            m_trimeshCreator = trimeshCreator;
            m_normalsService = normalsService;
    }

        /// <inheritdoc/>
        public bool Supports(ISurface surface) => surface is ITrimesh;

        /// <inheritdoc/>
        public ISurface Execute(ISurface surface, string name)
        {
            return Execute(surface as ITrimesh, name);
        }

        /// <inheritdoc/>
        public ITrimesh Execute(ITrimesh trimesh, string name)
        {
            var triangleVertexIndices = trimesh.TriangleVertexIndices;
            var vertices = trimesh.Vertices.Select(p => p.Clone());

            var normals = m_normalsService.GetNormals(trimesh);
            ITrimesh result = (normals == null) ?
                m_trimeshCreator.Create(triangleVertexIndices, vertices, name) :
                m_trimeshCreator.Create(triangleVertexIndices, vertices, normals.Select(n => n.Clone()), name);
            result.Material = trimesh.Material;
            return result;
        }
    }
}
