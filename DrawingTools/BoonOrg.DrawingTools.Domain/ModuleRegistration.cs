// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;
using BoonOrg.Registrations;
using BoonOrg.Storage;

using BoonOrg.DrawingTools.Picking;
using BoonOrg.DrawingTools.ColorMapping;
using BoonOrg.DrawingTools.Rendering;
using BoonOrg.DrawingTools.Services;
using BoonOrg.DrawingTools.Textures;

namespace BoonOrg.DrawingTools.Domain
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<AttributeModifierService, IAttributeModifierService>();

            registrar.RegisterType<ColorPicker, IColorPicker>();
            registrar.RegisterTypeAsSingleton<TextureService, ITextureService>();
            registrar.RegisterTypeAsSingleton<BitmapTextureSkyHook, IBitmapTextureSkyHook>();
            registrar.RegisterTypeAsSingleton<ViewContainer, IViewContainer>();

            registrar.RegisterType<BitmapTexture, IBitmapTexture>();
            registrar.RegisterType<ColorPin, IColorPin>();
            registrar.RegisterType<ColorSet, IColorSet>();
            registrar.RegisterType<ColorSetService, IColorSetService>();
            registrar.RegisterType<TextureCoordinate, ITextureCoordinate>();
            registrar.RegisterType<TriangleMeshGraphicsState, ITriangleMeshGraphicsState>();

            registrar.RegisterType<RenderDataSynchronizer, IRenderDataSynchronizer>();
            registrar.RegisterType<Camera, ICamera>();
            registrar.RegisterType<View, IView>();
            registrar.RegisterType<ViewCreator, IViewCreator>();
            registrar.RegisterType<ViewContainerCreator, IInitialDocumentContentCreator>();
            registrar.RegisterType<Viewport, IViewport>();
            registrar.RegisterType<RenderItemContainer, IRenderItemContainer>();
            registrar.RegisterType<RepresentationFactory, IRepresentationFactory>();
        }
    }
}
