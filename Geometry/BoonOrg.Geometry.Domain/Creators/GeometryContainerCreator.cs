// (c) 2018 Roland Boon

using System;

using BoonOrg.Identification;
using BoonOrg.Storage;

namespace BoonOrg.Geometry.Domain.Creators
{
    internal sealed class GeometryContainerCreator : IInitialDocumentContentCreator
    {
        private readonly Func<IGeometryContainer> m_geometryContainerFunc;

        public GeometryContainerCreator(Func<IGeometryContainer> geometryContainerFunc)
        {
            m_geometryContainerFunc = geometryContainerFunc;
        }

        public IIdentifiable Create() => m_geometryContainerFunc();
    }
}
