// (c) 2023, 2024 Roland Boon

using System;
using System.Threading;

using BoonOrg.Commands;
using BoonOrg.Commands.Responses;
using BoonOrg.DrawingTools.Rendering;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Services;

using BoonOrg.Surfaces.Commands;

namespace BoonOrg.Surfaces.Logic.CommandHandlers
{
    internal sealed class CommandHandlerToggleFacetedTriangles : ICommandHandler<CommandToggleFacetedTriangles, IResponse>
    {
        private readonly IAttributeModifierService m_attributeModifierService;
        private readonly IViewContainer m_viewContainer;
        private readonly IGeometryFinder m_geometryFinder;

        public CommandHandlerToggleFacetedTriangles(IAttributeModifierService attributeModifierService,
            IViewContainer viewContainer,
            IGeometryFinder geometryFinder)
        {
            m_attributeModifierService = attributeModifierService;
            m_viewContainer = viewContainer;
            m_geometryFinder = geometryFinder;
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, ICommand command)
        {
            return HandleCommand(commandExecuter, (CommandToggleFacetedTriangles)command);
        }

        public IResponse HandleCommand(ICommandExecuter commandExecuter, CommandToggleFacetedTriangles command)
        {
            var view = m_viewContainer.ActiveView;
            var geometry = m_geometryFinder.Find(command.Id);

            var attributes = view.Attributes;

            var drawSurfaceAttribute = attributes.GetAttribute<IDrawSurfaceAttribute>(geometry);
            var representation = attributes.GetAttribute<ISurfaceRepresentation>(geometry);

            if (view == null || geometry == null || drawSurfaceAttribute == null || representation == null)
            {
                return new Response { Success = false };
            }

            var selectedProperty = drawSurfaceAttribute.SelectedProperty;
            var propertyNames = representation.PropertyNames;

            drawSurfaceAttribute.ShowProperty = false;
            drawSurfaceAttribute.SelectedProperty = -1;
            drawSurfaceAttribute.FacetedTriangles = !drawSurfaceAttribute.FacetedTriangles;

            m_attributeModifierService.InformAttributeChanged(geometry);

            view.RenderItems.Remove(geometry);

            view.Attributes.RemoveAttribute<ITriangleMeshGraphicsState>(geometry);
            view.Attributes.RemoveAttribute<ISurfaceRepresentation>(geometry);

            view.RenderItems.Add(geometry);

            // Recompute properties
            while (true)
            {
                var renderItem = view.RenderItems.Find(geometry);
                if (renderItem == null)
                {
                    Thread.Sleep(20);
                    continue;
                }

                representation = view.Attributes.GetAttribute<ISurfaceRepresentation>(geometry);

                foreach (var propertyName in propertyNames)
                {
                    representation.AddProperty(propertyName);
                }

                break;
            }

            drawSurfaceAttribute.SelectedProperty = selectedProperty;
            drawSurfaceAttribute.ShowProperty = false;

            m_attributeModifierService.InformAttributeChanged(geometry);

            return new Response { Success = true };
        }
    }
}
