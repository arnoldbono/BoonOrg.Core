// (c) 2023 Roland Boon

using System;
using System.Reactive;

using BoonOrg.Geometry;

namespace BoonOrg.DrawingTools.Rendering
{
    /// <summary>
    /// This container has all the render items that need to be drawn by one view.
    /// </summary>
    public interface IRenderItemContainer
    {
        RenderItem[] RenderItems { get; }

        IGeometry[] Items { get; }

        IObservable<Unit> Updated { get; }

        void Add(RenderItem renderItem);

        void AddOrReplace(RenderItem renderItem);

        void Remove(RenderItem renderItem);

        void Remove(IGeometry geometry);

        void ReplaceRenderer(IGeometry item);

        void ScheduleRendererUpdate(RenderItem renderItem, IRendererUpdateRequest rendererUpdateRequest);

        void Clear();

        RenderItem Find(IGeometry geometry);

        void PopRendererScheduledForUpdate();

        RenderItem[] PopRenderItemsToUpdate();

        // RenderCollection

        void ProcessRenderersToClose();

        void Add(IGeometry item);

        IGeometry[] PopNewItems();
    }
}