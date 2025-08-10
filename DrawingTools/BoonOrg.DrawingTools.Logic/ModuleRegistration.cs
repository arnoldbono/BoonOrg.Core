// (c) 2023 Roland Boon

using System;

using BoonOrg.Registrations;

using BoonOrg.DrawingTools.Persistence;

namespace BoonOrg.DrawingTools.Logic
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<TrimeshPropertyTextureCoordinatesService, ITrimeshPropertyTextureCoordinatesService>();

            registrar.RegisterType<ColorSetXmlReader, IColorSetXmlReader>();
        }
    }
}
