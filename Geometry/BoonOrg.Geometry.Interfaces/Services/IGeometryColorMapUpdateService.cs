// (c) 2024 Roland Boon

using System;

namespace BoonOrg.Geometry.Services
{
    public interface IGeometryColorMapUpdateService
    {
        /// <summary>
        /// The color map with given index is changed.
        /// </summary>
        IObservable<int> ColorMapChanged { get; }

        /// <summary>
        /// The color map with given index is selected.
        /// </summary>
        IObservable<int> ColorMapSelected { get; }

        /// <summary>
        /// Inform listeners that the color map with index <paramref name="colorMap"/> is changed.
        /// </summary>
        /// <param name="colorMap">The index of the color map</param>
        void InformColorMapChanged(int colorMap);

        /// <summary>
        /// Inform listeners that the color map with index <paramref name="colorMap"/> is selected.
        /// </summary>
        /// <param name="colorMap">The index of the color map</param>
        void InformColorMapSelected(int colorMap);

    }
}
