// (c) 2023, 2024 Roland Boon

using System;

using BoonOrg.Geometry.Generators;
using BoonOrg.Registrations;
using BoonOrg.Commands;

using BoonOrg.Surfaces.Domain;
using BoonOrg.Surfaces.Commands;

namespace BoonOrg.Surfaces.Addin
{
    public class ModuleRegistration : IModuleRegistration
    {
        public string Application => string.Empty;

        public string Module => @"BoonOrg.Surfaces";

        public string[] ModuleDependencies => new string[] { @"BoonOrg.Geometry" };

        public void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterType<FactoryGrid2D, IFactoryGrid2D>();
            registrar.RegisterType<IndexedPropertyAttribute<IPropertyValueDouble>, IIndexedPropertyAttribute<IPropertyValueDouble>>();
            registrar.RegisterType<IndexedPropertyAttribute<IPropertyValueTextureMap>, IIndexedPropertyAttribute<IPropertyValueTextureMap>>();
            registrar.RegisterType<ConvertToIndexedTriangleMesh, IConvertToIndexedTriangleMesh>();
            registrar.RegisterType<GeometryRepresentation, ISurfaceRepresentation>();

            registrar.RegisterType<PropertyFactory, IPropertyFactory>();
            registrar.RegisterType<CoordinateXPropertyProducer, IPropertyProducer>();
            registrar.RegisterType<CoordinateYPropertyProducer, IPropertyProducer>();
            registrar.RegisterType<ProductPropertyProducer, IPropertyProducer>();
            registrar.RegisterType<HeightPropertyProducer, IPropertyProducer>();
            registrar.RegisterType<TextureMapPropertyProducer, IPropertyProducer>();
            registrar.RegisterType<PropertyValueDoubleProducer>();
            registrar.RegisterType<PropertyProducer<IPropertyValueDouble>>();
            registrar.RegisterType<PropertyProducer<IPropertyValueTextureMap>>();
            registrar.RegisterType<PropertyValueDouble, IPropertyValueDouble>();
            registrar.RegisterType<PropertyValueTextureMap, IPropertyValueTextureMap>();

            registrar.RegisterType<Grid2DToMeshGeometryGenerator, IMeshGeometryGenerator>();

            Logic.ModuleRegistration.Register(registrar);

            // ROBOCOP
            // TODO: Add Surfaces.Persistance
        }

        public void PostRegistration(IResolver resolver)
        {
           var commandRegistrar = resolver.Resolve<ICommandHandlerRegistrar>();
            Logic.ModuleRegistration.CommandHandlerRegistration(commandRegistrar);
        }
    }
}
