// (c) 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Storage.Attributes
{
    internal abstract class AttributeStorageProvider<T> : IGeometryAttributeStorageProvider<T> where T : class, IGeometryAttribute
    {
        public void Read(BinaryReader reader, object geometryAttribute)
        {
            Read(reader, (T)geometryAttribute);
        }

        public abstract void Read(BinaryReader reader, T geometryAttribute);

        public void Write(BinaryWriter writer, object geometryAttribute)
        {
            Write(writer, (T)geometryAttribute);
        }

        public abstract void Write(BinaryWriter writer, T geometryAttribute);
    }
}
