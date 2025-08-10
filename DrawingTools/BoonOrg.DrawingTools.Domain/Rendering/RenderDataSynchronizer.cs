// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;

using BoonOrg.DrawingTools.Textures;
using BoonOrg.Geometry.Services;

namespace BoonOrg.DrawingTools.Rendering
{
    public sealed class RenderDataSynchronizer : IRenderDataSynchronizer
    {
        private readonly IGeometryEventService m_geometryEventService;
        private readonly IBitmapTextureSkyHook m_bitmapTextureSkyHook;

        private IView m_view;
        private IRendererFactory m_rendererFactory;
        private Thread m_thread;
        private bool m_stop;
        private ManualResetEvent m_resetEvent;

        public RenderDataSynchronizer(IGeometryEventService geometryEventService, IBitmapTextureSkyHook bitmapTextureSkyHook)
        {
            m_geometryEventService = geometryEventService;
            m_bitmapTextureSkyHook = bitmapTextureSkyHook;
        }

        public bool Running => m_thread != null;

        public void Start(IView view, IRendererFactory rendererFactory)
        {
            m_view = view;
            m_rendererFactory = rendererFactory;

            m_thread = new Thread(_ => Synchronize())
            {
                Name = @"RenderDataSynchronizer"
            };
            m_thread.Start();
        }

        public void Stop()
        {
            m_stop = true;
            m_resetEvent?.Set();
        }

        private void Synchronize()
        {
            var renderItemContainer = m_view.RenderItems;

            m_resetEvent = new ManualResetEvent(false);
            var subscription = renderItemContainer.Updated.Subscribe(_ => m_resetEvent?.Set());

            while (!m_stop)
            {
                var renderItemsToAdd = new List<RenderItem>();
                var renderItemsToUpdate = renderItemContainer.PopRenderItemsToUpdate().ToList();

                var geometries = m_view.RenderItems.PopNewItems();
                var newItems = new List<RenderItem>();
                foreach (var geometry in geometries)
                {
                    var renderItem = renderItemContainer.Find(geometry);
                    if (renderItem != null)
                    {
                        renderItemContainer.Remove(renderItem);
                        renderItemsToUpdate.Add(renderItem);
                        continue;
                    }

                    m_view.Attributes.AddDefaultAttributes(geometry);

                    var renderer = m_rendererFactory.Create(geometry, m_view);
                    renderItem = new RenderItem(geometry, renderer);

                    newItems.Add(renderItem);
                    renderItemsToUpdate.Add(renderItem);
                }

                foreach (var renderItem in renderItemsToUpdate)
                {
                    renderItem.Renderer.Update(m_bitmapTextureSkyHook);
                    renderItemsToAdd.Add(renderItem);
                }

                // Add the renderer at the end. That way, we are not updating while the renderer is rendering.
                foreach (var renderItem in renderItemsToAdd)
                {
                    renderItemContainer.Add(renderItem);
                    if (newItems.Contains(renderItem))
                    {
                        m_geometryEventService.Added(renderItem.Geometry);
                    }
                }

                m_resetEvent.WaitOne();
                m_resetEvent.Reset();
            }

            subscription.Dispose();
            m_resetEvent.Dispose();
            m_resetEvent = null;
            m_stop = false;
        }
    }
}
 