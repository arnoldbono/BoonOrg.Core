// (c) 2023 Roland Boon

namespace BoonOrg.DrawingTools.Rendering
{
    public interface IRenderDataSynchronizer
    {
        bool Running { get; }

        void Start(IView view, IRendererFactory fendererFactory);

        void Stop();
    }
}