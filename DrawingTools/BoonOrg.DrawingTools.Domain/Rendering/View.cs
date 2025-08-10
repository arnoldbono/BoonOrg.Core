// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Attributes;
using BoonOrg.Identification;

namespace BoonOrg.DrawingTools.Rendering
{
    public sealed class View : IView
    {
        private readonly IIdentity m_identity;
        private readonly IRenderItemContainer m_renderItemContainer;
        private readonly IGeometryAttributeContainer m_geometryAttributeContainer;

        public View(IIdentity identity, IRenderItemContainer renderItemContainer, IGeometryAttributeContainer geometryAttributeContainer)
        {
            m_identity = identity;
            m_renderItemContainer = renderItemContainer;
            m_geometryAttributeContainer = geometryAttributeContainer;
        }

        public IIdentity Identification => m_identity;

        public IBoundingBox BoundingBox { get; set; }

        public ICamera Camera { get; private set; }

        public IViewport Viewport { get; private set; }

        public IRenderItemContainer RenderItems => m_renderItemContainer;

        public IGeometryAttributeContainer Attributes => m_geometryAttributeContainer;

        public void Initialize(float width, float height)
        {
            Viewport = new Viewport
            {
                Width = width,
                Height = height
            };

            Camera = new Camera(Viewport);
        }

    }
}
