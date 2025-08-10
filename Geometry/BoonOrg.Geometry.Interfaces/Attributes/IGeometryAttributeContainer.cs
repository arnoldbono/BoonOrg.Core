// (c) 2018 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Attributes
{
    public interface IGeometryAttributeContainer : IIdentifiable
    {
        IEnumerable<Guid> ItemIds { get; }

        bool HasAttributes(Guid itemId);

        bool HasAttributes(IIdentifiable item);

        bool HasAttributeOfType(Guid itemId, Type type);

        bool HasAttributeOfType(IIdentifiable item, Type type);

        bool HasAttribute<T>(Guid itemId) where T : IGeometryAttribute;

        void Clear();

        IEnumerable<IGeometryAttribute> GetAttributes(IIdentifiable item);

        IEnumerable<IGeometryAttribute> GetAttributes(Guid itemId);

        T GetAttribute<T>(IIdentifiable item) where T : IGeometryAttribute;

        T GetAttribute<T>(Guid itemId) where T : IGeometryAttribute;

        bool AddAttribute<T>(IIdentifiable item, T attribute) where T : IGeometryAttribute;

        bool AddAttribute<T>(Guid itemId, T attribute) where T : IGeometryAttribute;

        bool AddAttributeOfType(IIdentifiable item, IGeometryAttribute attribute, Type type);

        void UpdateAttribute<T>(IIdentifiable item, T attribute) where T : IGeometryAttribute;

        void UpdateAttribute<T>(Guid itemId, T attribute) where T : IGeometryAttribute;

        void RemoveAttribute<T>(IIdentifiable item) where T : IGeometryAttribute;

        void RemoveAttribute<T>(Guid itemId) where T : IGeometryAttribute;

        void RemoveAttributes(IIdentifiable item);

        void RemoveAttributes(Guid itemId);

        void AddDefaultAttributes(IIdentifiable item);
    }
}
