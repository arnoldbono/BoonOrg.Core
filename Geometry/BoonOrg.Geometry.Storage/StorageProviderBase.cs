// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Storage;
using BoonOrg.Identification;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Storage
{
    internal abstract class StorageProviderBase<T> : StorageProvider<T> where T : class, IIdentifiable
    {
        private readonly IPointCreator m_pointCreator;
        private readonly IVectorCreator m_vectorCreator;

        public StorageProviderBase(Func<T> func,
            IStorageIdentifier storageIdentifier,
            IPointCreator pointCreator,
            IVectorCreator vectorCreator) : base(func, storageIdentifier)
        {
            m_pointCreator = pointCreator;
            m_vectorCreator = vectorCreator;
        }

        protected void Write(IPoint point, BinaryWriter writer)
        {
            writer.Write(point.X);
            writer.Write(point.Y);
            writer.Write(point.Z);
        }

        protected void Write(IVector vector, BinaryWriter writer)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        protected IVector ReadVector(BinaryReader reader)
        {
            return m_vectorCreator.Create(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }

        protected IPoint ReadPoint(BinaryReader reader)
        {
            return m_pointCreator.Create(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }
    }
}
