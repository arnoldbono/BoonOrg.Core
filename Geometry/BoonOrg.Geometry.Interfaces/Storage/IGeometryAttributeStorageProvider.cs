// (c) 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Storage
{
    public interface IGeometryAttributeStorageProvider<T> : IGeometryAttributeStorageProvider where T : class, IGeometryAttribute
    {
        void Read(BinaryReader reader, T geometryAttribute);

        void Write(BinaryWriter writer, T geometryAttribute);
    }

    public interface IGeometryAttributeStorageProvider
    {
        void Read(BinaryReader reader, object geometryAttribute);

        void Write(BinaryWriter writer, object geometryAttribute);
    }
}
