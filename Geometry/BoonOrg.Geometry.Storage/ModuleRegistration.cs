// (c) 2017, 2018, 2019, 2023 Roland Boon

using System;

using BoonOrg.Registrations;
using BoonOrg.Storage;

using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Storage.Attributes;

namespace BoonOrg.Geometry.Storage
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterType<LayerGridImporter, ILayerGridImporter>();
            registrar.RegisterType<MeshImporter, IMeshImporter>();

            registrar.RegisterType<GeometryContainerStorageProvider, IStorageProvider<IGeometryContainer>>();
            registrar.RegisterType<LayerGridStorageProvider, IStorageProvider<ILayerGrid>>();
            registrar.RegisterType<TrimeshStorageProvider, IStorageProvider<ITrimesh>>();
            registrar.RegisterType<TetrahedraShapeStorageProvider, IStorageProvider<ICompoundShape<ITetrahedron>>>();
            registrar.RegisterType<AxesStorageProvider, IStorageProvider<IAxes>>();
            registrar.RegisterType<PlaneStorageProvider, IStorageProvider<IPlane>>();
            registrar.RegisterType<GeometryAttributeContainerStorageProvider, IStorageProvider<IGeometryAttributeContainer>>();

            registrar.RegisterType<OpacityAttributeStorageProvider, IGeometryAttributeStorageProvider<IOpacityAttribute>> ();
            registrar.RegisterType<SolidMaterialAttributeStorageProvider, IGeometryAttributeStorageProvider<ISolidMaterialAttribute>>();
            registrar.RegisterType<DrawSurfaceAttributeStorageProvider, IGeometryAttributeStorageProvider<IDrawSurfaceAttribute>> ();
        }

        public static void StorageRegistration(IStorageRegistration storageRegistration)
        {
            storageRegistration.Register<IGeometryContainer>();
            storageRegistration.Register<ILayerGrid>();
            storageRegistration.Register<ITrimesh>();
            storageRegistration.Register<ICompoundShape<ITetrahedron>>();
            storageRegistration.Register<IAxes>();
            storageRegistration.Register<IPlane>();
            storageRegistration.Register<IGeometryAttributeContainer>();
        }
    }
}
