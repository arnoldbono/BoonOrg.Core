// (c) 2017, 2018, 2019, 2023 Roland Boon

using System;

using BoonOrg.Registrations;
using BoonOrg.Actions;
using BoonOrg.Functions;
using BoonOrg.Commands;

using BoonOrg.Geometry.Commands;
using BoonOrg.Geometry.Services;
using BoonOrg.Geometry.Logic.CommandHandlers;
using BoonOrg.Geometry.Logic.Services;
using BoonOrg.Geometry.Logic.Operations;

namespace BoonOrg.Geometry.Logic
{
    public class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<GeometrySelectionService, IGeometrySelectionService>();
            registrar.RegisterTypeAsSingleton<GeometryLocatorService, IGeometryLocatorService>();
            registrar.RegisterTypeAsSingleton<GeometryEventService, IGeometryEventService>();
            registrar.RegisterTypeAsSingleton<TrimeshPropertyNormalsService, ITrimeshPropertyNormalsService>();

            registrar.RegisterType<LayerGridFromTerrainGenerator, ILayerGridFromTerrainGenerator>();
            registrar.RegisterType<LayerGridCopier, ISurfaceCopy>();
            registrar.RegisterType<TrimeshCopier, ISurfaceCopy>();
            registrar.RegisterType<SurfaceCopier, ISurfaceCopier>();
            registrar.RegisterType<SurfaceRenamer, ISurfaceRenamer>();
            registrar.RegisterType<LayerGridTranslator, ISurfaceTranslator>();
            registrar.RegisterType<TrimeshTranslator, ISurfaceTranslator>();
            registrar.RegisterType<PlaneIntersectionVolumeCalculator, IPlaneIntersectionVolumeCalculator>();
            registrar.RegisterType<LayerGridPlaneIntersectionToTetrahedra, IPlaneIntersectionToTetrahedra<ILayerGrid>>();
            registrar.RegisterType<TrimeshPlaneIntersectionToTetrahedra, IPlaneIntersectionToTetrahedra<ITrimesh>>();
            registrar.RegisterType<TetrahedraVolumeCalculator, ITetrahedraVolumeCalculator>();
            registrar.RegisterType<TetrahedraFinder, ITetrahedraFinder>();
            registrar.RegisterType<IntersectionCalculator, IIntersectionCalculator>();
            registrar.RegisterType<TetrahedraToTrimeshConverter, ITetrahedraToTrimeshConverter>();
            registrar.RegisterType<LayerGridToTrimeshTransformer, ILayerGridToTrimeshTransformer>();
            registrar.RegisterType<TrimeshPlaneCutoffCalculate, ITrimeshPlaneCutoffCalculator>();
            registrar.RegisterType<VectorLogic, IVectorLogic>();
            registrar.RegisterType<TriangleContainerNormalizer, ITriangleContainerNormalizer>();
            registrar.RegisterType<GroupAdder, IGroupAdder>();
            registrar.RegisterType<SmoothedTrimeshCreator, ISmoothedTrimeshCreator>();

            registrar.RegisterType<GeometryDeleter, IGeometryDeleter>();
            registrar.RegisterType<GeometryAdder, IGeometryAdder>();
            registrar.RegisterType<GeometryFinder, IGeometryFinder>();

            registrar.RegisterType<CommandHandlerCopySurface, ICommandHandler<CommandCopySurface, IResponse>>();
            registrar.RegisterType<CommandHandlerCreatePlane, ICommandHandler<CommandCreatePlane, IResponse>>();
            registrar.RegisterType<CommandHandlerCreateCircle, ICommandHandler<CommandCreateCircle, IResponse>>();
            registrar.RegisterType<CommandHandlerConvertLayerGridToTrimesh, ICommandHandler<CommandConvertLayerGridToTrimesh, IResponse>>();
            registrar.RegisterType<CommandHandlerNormalizeTrimesh, ICommandHandler<CommandNormalizeTrimesh, IResponse>>();
            registrar.RegisterType<CommandHandlerDisplaceSurface, ICommandHandler<CommandDisplaceSurface, IResponse>>();

            registrar.RegisterType<CommandCreateCircle>(); // TODO Remove

            registrar.RegisterType<OperationExportTrimesh>();
            registrar.RegisterType<OperationExportTrimeshes>();

            registrar.RegisterType<VectorMapping, IIdentifiableMapping<IVector>>();
            registrar.RegisterType<PointMapping, IIdentifiableMapping<IPoint>>();
        }

        public static void FunctionRegistration(IFunctionCallRegistrar registrar)
        {
            registrar.Add<OperationExportTrimesh>(@"ExportTrimesh");
            registrar.Add<OperationExportTrimeshes>(@"ExportTrimeshes");
        }

        public static void CommandHandlerRegistration(ICommandHandlerRegistrar commandRegistrar)
        {
            commandRegistrar.Register<CommandCopySurface, IResponse>();
            commandRegistrar.Register<CommandCreatePlane, IResponse>();
            commandRegistrar.Register<CommandCreateCircle, IResponse>();
            commandRegistrar.Register<CommandConvertLayerGridToTrimesh, IResponse>();
            commandRegistrar.Register<CommandNormalizeTrimesh, IResponse>();
            commandRegistrar.Register<CommandDisplaceSurface, IResponse>();
        }

        public static void MenuItemRegistration(IAddinMenuItemRegistrar menuItemRegistrar)
        {
            menuItemRegistrar.AddActionCommand<CommandCreateCircle>(@"/Edit/_Add/_Circle");
        }
    }
}
