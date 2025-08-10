// (c) 2017, 2018, 2019, 2023 Roland Boon

using System;

using BoonOrg.Registrations;
using BoonOrg.Commands;

using BoonOrg.Surfaces.Commands;
using BoonOrg.Surfaces.Logic.CommandHandlers;

namespace BoonOrg.Surfaces.Logic
{
    public class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterType<CommandHandlerToggleFacetedTriangles, ICommandHandler<CommandToggleFacetedTriangles, IResponse>>();
        }

        public static void CommandHandlerRegistration(ICommandHandlerRegistrar commandRegistrar)
        {
            commandRegistrar.Register<CommandToggleFacetedTriangles, IResponse>();
        }
    }
}
