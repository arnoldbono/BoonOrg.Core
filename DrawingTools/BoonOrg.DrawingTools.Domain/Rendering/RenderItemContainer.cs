// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using BoonOrg.Geometry;
using BoonOrg.Geometry.Events;
using BoonOrg.Geometry.Services;

namespace BoonOrg.DrawingTools.Rendering
{
    public sealed class RenderItemContainer : IRenderItemContainer
    {
        private readonly IGeometryEventService m_geometryEventService;

        private readonly List<IGeometry> m_newItems = new();
        private readonly Dictionary<IGeometry, RenderItem> m_renderItems = new();
        private readonly List<RenderItem> m_renderItemsScheduledForUpdate = new();
        private readonly List<RenderItem> m_renderItemsToUpdate = new();
        private readonly List<IRenderer> m_renderersToClose = new();
        private readonly Subject<Unit> m_subject = new();

        public IObservable<Unit> Updated => m_subject;

        public RenderItemContainer(IGeometryEventService geometryEventService)
        {
            m_geometryEventService = geometryEventService;

            m_geometryEventService.Events.Subscribe(x =>
            {
                switch (x)
                {
                    case IGeometryModifiedEvent e:
                        lock (m_renderItems)
                        {
                            foreach (var item in x.Items)
                            {
                                if (!m_renderItems.ContainsKey(item))
                                {
                                    continue;
                                }

                                var renderItem = m_renderItems[item];
                                ScheduleRendererUpdate(renderItem, new RendererUpdateRequest
                                {
                                    UpdateVertices = e.UpdateVertices,
                                    UpdatePropertyValues = e.UpdatePropertyValues,
                                    UpdateColorSet = e.UpdateColorMap,
                                    UpdateUvs = false,
                                    AttributeChange = e.AttributeChange
                                });
                            }
                        }
                        break;
                    case IGeometryDeletedEvent:
                        lock (m_renderItems)
                        {
                            foreach (var item in x.Items)
                            {
                                m_renderItems.Remove(item);
                            }
                        }
                        break;
                    case IGeometryDocumentCloseEvent:
                        lock (m_renderItems)
                        {
                            m_renderItems.Clear();
                        }
                        break;
                }
                m_subject.OnNext(Unit.Default);
            });
        }

        public RenderItem Find(IGeometry geometry)
        {
            if (geometry == null)
            {
                return null;
            }

            lock (m_renderItems)
            {
                if (m_renderItems.ContainsKey(geometry))
                {
                    return m_renderItems[geometry];
                }
            }

            return null;
        }

        public void Add(RenderItem renderItem)
        {
            if (renderItem == null)
            {
                return;
            }

            lock (m_renderItems)
            {
                if (m_renderItems.ContainsKey(renderItem.Geometry))
                {
                    m_renderItems[renderItem.Geometry] = renderItem;
                }
                else
                {
                    m_renderItems.Add(renderItem.Geometry, renderItem);
                }
            }

            m_subject.OnNext(Unit.Default);
        }

        // Thread safe
        public void AddOrReplace(RenderItem renderItem)
        {
            if (renderItem == null)
            {
                return;
            }

            ScheduleRendererUpdate(renderItem, new RendererUpdateRequest());
        }

        public void Remove(RenderItem renderItem)
        {
            if (renderItem == null)
            {
                return;
            }

            Remove(renderItem.Geometry);
        }

        public void Remove(IGeometry geometry)
        {
            if (geometry == null)
            {
                return;
            }

            lock (m_newItems)
            {
                m_newItems.Remove(geometry);
            }

            lock (m_renderItems)
            {
                if (!m_renderItems.ContainsKey(geometry))
                {
                    return;
                }

                var renderer = m_renderItems[geometry].Renderer;
                m_renderItems.Remove(geometry);

                lock (m_renderersToClose)
                {
                    m_renderersToClose.Add(renderer);
                }
            }

            m_subject.OnNext(Unit.Default);
        }

        private void UpdateRenderer(IGeometry geometry, IRendererUpdateRequest rendererUpdateRequest)
        {
            if (geometry == null)
            {
                return;
            }

            var renderItem = Find(geometry);
            if (renderItem == null)
            {
                return;
            }

            ScheduleRendererUpdate(renderItem, rendererUpdateRequest ?? new RendererUpdateRequest());

            m_geometryEventService.Modified(geometry, true, true, true, true);
        }

        public void ReplaceRenderer(IGeometry geometry)
        {
            UpdateRenderer(geometry, new RendererUpdateRequest{ ReplaceRenderer = true });
        }

        public void ScheduleRendererUpdate(RenderItem renderItem, IRendererUpdateRequest rendererUpdateRequest)
        {
            lock (m_renderItemsScheduledForUpdate)
            {
                if (!m_renderItemsScheduledForUpdate.Any(x => x == renderItem))
                {
                    if (rendererUpdateRequest.ReplaceRenderer)
                    {
                        Remove(renderItem);
                        Add(renderItem.Geometry);
                    }
                    else
                    {
                        renderItem.Renderer.UpdateRequest = rendererUpdateRequest;
                        m_renderItemsScheduledForUpdate.Add(renderItem);
                    }
                }
            }
        }

        public RenderItem[] RenderItems
        {
            get
            {
                lock (m_renderItems)
                {
                    return m_renderItems.Values.ToArray();
                }
            }
        }

        public void ProcessRenderersToClose() //  Must be called from the UI thread
        {
            IRenderer[] renderersToClose;

            lock (m_renderersToClose)
            {
                renderersToClose = m_renderersToClose.ToArray();
                if (!renderersToClose.Any())
                {
                    return;
                }

                m_renderersToClose.Clear();
            }

            bool update = false;

            foreach (var renderer in renderersToClose)
            {
                renderer.Close();
                update = true;
            }

            if (update)
            {
                m_subject.OnNext(Unit.Default);
            }

        }

        public void Clear()
        {
            lock (m_renderItems)
            {
                foreach (var renderItem in m_renderItems.Values)
                {
                    renderItem.Renderer.Close();
                }

                m_renderItems.Clear();
            }

            lock (m_newItems)
            {
                m_newItems.Clear();
            }
        }

        // Must be called from UI thread
        public void PopRendererScheduledForUpdate()
        {
            RenderItem[] renderItemsToUpdateRenderer;

            lock (m_renderItemsScheduledForUpdate)
            {
                renderItemsToUpdateRenderer = m_renderItemsScheduledForUpdate.ToArray();
                m_renderItemsScheduledForUpdate.Clear();
            }

            UpdateRenderer(renderItemsToUpdateRenderer);
        }

        private void UpdateRenderer(RenderItem[] renderItemsToUpdate)
        {
            if (!renderItemsToUpdate.Any())
            {
                return;
            }

            lock (m_renderItemsToUpdate)
            {
                foreach (var renderItemUpdate in renderItemsToUpdate)
                {
                    if (!m_renderItemsToUpdate.Contains(renderItemUpdate))
                    {
                        m_renderItemsToUpdate.Add(renderItemUpdate);
                    }
                }
            }

            m_subject.OnNext(Unit.Default);
        }


        // Can be called from worker thread
        public RenderItem[] PopRenderItemsToUpdate()
        {
            lock (m_renderItemsToUpdate)
            {
                var renderItemsToUpdate = m_renderItemsToUpdate.ToArray();
                m_renderItemsToUpdate.Clear();
                return renderItemsToUpdate;
            }
        }

        public void Add(IGeometry geometry)
        {
            if (geometry == null)
            {
                return;
            }

            lock (m_renderItems)
            {
                if (m_renderItems.ContainsKey(geometry))
                {
                    return;
                }
            }

            var updated = false;

            lock (m_newItems)
            {
                if (!m_newItems.Contains(geometry))
                {
                    m_newItems.Add(geometry);
                    updated = true;
                }
            }

            if (updated)
            {
                m_geometryEventService.Added(geometry);
            }
        }

        public IGeometry[] Items
        {
            get
            {
                lock (m_renderItems)
                {
                    return m_renderItems.Keys.ToArray();
                }
            }
        }

        public IGeometry[] PopNewItems()
        {
            lock (m_newItems)
            {
                var newItems = m_newItems.ToArray();
                m_newItems.Clear();
                return newItems;
            }
        }
    }
}
