// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Commands;
using BoonOrg.Identification;
using BoonOrg.Storage;

namespace BoonOrg.Geometry
{
    public interface IGeometryContainer : IIdentifiableCollection<IGeometry>, IGeometryOwner, IContainerHandover<IGeometryContainer>
    {
        bool Exists(string name);

        IGeometry Get(string name);

        IGeometry Get(Guid id);

        string GetUniqueName(string name);

        IEnumerable<T> GetList<T>() where T : IGeometry;
    }

    public static class DocumentExtender
    {
        public static IGeometryContainer GetGeometryContainer(this IDocument document) => document.Get<IGeometryContainer>();
    }
}
