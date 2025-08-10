// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces
{
    /// <summary>
    /// This class is a wrapper for the mesh so it can be stored as a geometry attribute.
    /// The mesh is an indexed triangle representation of the geometry.
    /// </summary>
    public interface ISurfaceRepresentation : IGeometryAttribute
    {
        IIndexedMesh<IndexedTriangle> Mesh { get; set; }

        int PropertyCount { get; }

        string[] PropertyNames { get; }

        IPropertyAttribute GetProperty(string propertyName);

        void AddProperty(string propertyName);

        void RemoveProperty(string propertyName);

        IRenderData CreateRenderData();

        IRenderData CreateRenderData(IPropertyAttribute property);
    }
}
