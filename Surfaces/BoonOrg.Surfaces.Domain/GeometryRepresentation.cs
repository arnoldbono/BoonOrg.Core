// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces.Domain
{
    public sealed class GeometryRepresentation : ISurfaceRepresentation
    {
        private readonly IRenderDataCreator m_renderDataCreator;
        private readonly IPropertyFactory m_propertyFactory;

        public IIndexedMesh<IndexedTriangle> Mesh { get; set; }

        public int PropertyCount => Mesh.Properties != null ? Mesh.Properties.Length : 0;

        public string[] PropertyNames => Mesh.Properties?.Select(x => x.PropertyType.Name).ToArray() ?? Array.Empty<string>();

        public GeometryRepresentation(IRenderDataCreator renderDataCreator,
            IPropertyFactory propertyFactory)
        {
            m_renderDataCreator = renderDataCreator;
            m_propertyFactory = propertyFactory;
        }

        public IPropertyAttribute GetProperty(string propertyName)
        {
            return Mesh.Properties?.SingleOrDefault(x => x.PropertyType.Name == propertyName);
        }

        public void AddProperty(string propertyName)
        {
            var properties = new List<IPropertyAttribute>();
            if (Mesh.Properties != null)
            {
                properties.AddRange(Mesh.Properties);
            }

            properties.Add(m_propertyFactory.Produce(Mesh.Vertices, propertyName));
            Mesh.Properties = properties.ToArray();
        }

        public void RemoveProperty(string propertyName)
        {
            var properties = new List<IPropertyAttribute>();
            if (Mesh.Properties != null)
            {
                properties.AddRange(Mesh.Properties);
            }

            properties.RemoveAll(x => string.Compare(x.PropertyType.Name, propertyName) == 0);
            Mesh.Properties = properties.ToArray();
        }

        public IRenderData CreateRenderData()
        {
            return CreateRenderData(Mesh.VerticesArray3(), Array.Empty<float>(), Mesh.NormalsArray3(), Mesh.ElementIndices());
        }

        public IRenderData CreateRenderData(IPropertyAttribute property)
        {
            return CreateRenderData(Mesh.VerticesArray3(), Mesh.PropertyArray(property), Mesh.NormalsArray3(), Mesh.ElementIndices());
        }

        private IRenderData CreateRenderData(float[] vertices, float[] propertyValues, float[] normals, int[] indices)
        {
            return m_renderDataCreator.Create(vertices, propertyValues, normals, indices);
        }
    }
}
