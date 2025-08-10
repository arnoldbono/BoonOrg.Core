// (c) 2017, 2018, 2019, 2023 Roland Boon

using System;

using BoonOrg.Registrations;
using BoonOrg.Storage;

using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Domain.Attributes;
using BoonOrg.Geometry.Domain.Creators;
using BoonOrg.Geometry.Domain.Generators;
using BoonOrg.Geometry.Domain.Visualization;
using BoonOrg.Geometry.Visualization;
using BoonOrg.Geometry.Services;
using BoonOrg.Geometry.Domain.Services;
using BoonOrg.Geometry.Events;
using BoonOrg.Geometry.Generators;
using BoonOrg.Geometry.Persistence;

namespace BoonOrg.Geometry.Domain
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<MeshGeometryService, IMeshGeometryService>();
            registrar.RegisterTypeAsSingleton<AttributeTypeRegistrations, IAttributeTypeRegistrations>();
            registrar.RegisterTypeAsSingleton<PropertyTypeService, IPropertyTypeService>();

            registrar.RegisterType<BoundingBox, IBoundingBox>();
            registrar.RegisterType<PointCreator, IPointCreator>();
            registrar.RegisterType<PointCreator, ICreator<IPoint>>();
            registrar.RegisterType<VectorCreator, IVectorCreator>();
            registrar.RegisterType<VectorCreator, ICreator<IVector>>();
            registrar.RegisterType<TetrahedronCreator, ITetrahedronCreator>();
            registrar.RegisterType<CompoundShapeCreator<ITetrahedron>, ICompoundShapeCreator<ITetrahedron>>();
            registrar.RegisterType<CompoundShape<ITetrahedron>, ICompoundShape<ITetrahedron>>();
            registrar.RegisterType<TrimeshCreator, ITrimeshCreator>();
            registrar.RegisterType<Triangle, ITriangle>();
            registrar.RegisterType<TriangleCreator, ITriangleCreator>();
            registrar.RegisterType<AreaCreator, IAreaCreator>();
            registrar.RegisterType<LayerGridCreator, ILayerGridCreator>();
            registrar.RegisterType<BarCreator, IBarCreator>();
            registrar.RegisterType<ConeCreator, IConeCreator>();
            registrar.RegisterType<CylinderCreator, ICylinderCreator>();
            registrar.RegisterType<PyramidCreator, IPyramidCreator>();
            registrar.RegisterType<EllipsoidCreator, IEllipsoidCreator>();
            registrar.RegisterType<PrismCreator, IPrismCreator>();
            registrar.RegisterType<PlaneCreator, IPlaneCreator>();
            registrar.RegisterType<TriangleContainerCreator, ITriangleContainerCreator>();
            registrar.RegisterType<ColorCreator, IColorCreator>();

            registrar.RegisterType<GeometryAttributeContainer, IGeometryAttributeContainer>();
            registrar.RegisterType<OpacityAttribute, IOpacityAttribute>();
            registrar.RegisterType<SolidMaterialAttribute, ISolidMaterialAttribute>();
            registrar.RegisterType<DrawSurfaceAttribute, IDrawSurfaceAttribute>();
            registrar.RegisterType<Circle, ICircle>();
            registrar.RegisterType<Cone, ICone>();
            registrar.RegisterType<Plane, IPlane>();
            registrar.RegisterType<Cylinder, ICylinder>();
            registrar.RegisterType<Ellipsoid, IEllipsoid>();
            registrar.RegisterType<GeometryContainer, IGeometryContainer>();
            registrar.RegisterType<RotatorAndTranslator, IRotatorAndTranslator>();
            registrar.RegisterType<Axes, IAxes>();
            registrar.RegisterType<LayerGrid, ILayerGrid>();
            registrar.RegisterType<Trimesh, ITrimesh>();
            registrar.RegisterType<TriangleContainer, ITriangleContainer>();
            registrar.RegisterType<Point, IPoint>();
            registrar.RegisterType<Polyline, IPolyline>();
            registrar.RegisterType<Vector, IVector>();
            registrar.RegisterType<Tetrahedron, ITetrahedron>();
            registrar.RegisterType<InstanceMatrices, IInstanceMatrices>();
            registrar.RegisterType<InstancedShape, IInstancedShape>();

            // Visualization
            registrar.RegisterType<AmbientLight, IAmbientLight>();
            registrar.RegisterType<Camera, ICamera>();
            registrar.RegisterType<Color, IColor>();
            registrar.RegisterType<Colour, IColour>();
            registrar.RegisterType<DirectionalLight, IDirectionalLight>();
            registrar.RegisterType<Material, IMaterial>();
            registrar.RegisterType<PbrProperty, IPbrProperty>();
            registrar.RegisterType<SpotLight, ISpotLight>();
            registrar.RegisterType<RenderData, IRenderData>();
            registrar.RegisterType<RenderDataCreator, IRenderDataCreator>();
            registrar.RegisterType<PropertyCreator, IPropertyCreator>();

            registrar.RegisterType<GeometryEventFactory, IGeometryEventFactory>();

            registrar.RegisterType<TrimeshExporterFactory, ITrimeshExporterFactory>();

            registrar.RegisterType<GeometryContainerCreator, IInitialDocumentContentCreator>();

            registrar.RegisterType<BasicMeshGeometryGenerator, IBasicMeshGeometryGenerator>();
            registrar.RegisterType<CompoundShapeMeshGeometryGenerator, IMeshGeometryGenerator>();
            registrar.RegisterType<InstancedShapeToMeshGeometryGenerator, IMeshGeometryGenerator>();
            registrar.RegisterType<LayerGridToMeshGeometryGenerator, IMeshGeometryGenerator>();
            registrar.RegisterType<PlaneToMeshGeometryGenerator, IMeshGeometryGenerator>();
            registrar.RegisterType<TetrahedraShapeToMeshGeometryGenerator, IMeshGeometryGenerator>();
            registrar.RegisterType<TrimeshToMeshGeometryGenerator, IMeshGeometryGenerator>();
        }
    }
}
