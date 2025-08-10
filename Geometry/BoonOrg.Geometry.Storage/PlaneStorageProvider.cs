// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Storage;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Storage
{
    internal sealed class PlaneStorageProvider : StorageProviderBase<IPlane>
    {
        public PlaneStorageProvider(Func<IPlane> func,
            IStorageIdentifier storageIdentifier,
            IVectorCreator vectorCreator,
            IPointCreator pointCreator) : base(func, storageIdentifier, pointCreator, vectorCreator)
        {
        }

        public override void ReadContent(BinaryReader reader, IDocument document, IPlane plane)
        {
            plane.Length = reader.ReadDouble();
            plane.Width = reader.ReadDouble();

            plane.Center.Set(ReadPoint(reader));
            plane.Normal.Set(ReadVector(reader));
        }

        public override void WriteContent(IPlane plane, BinaryWriter writer)
        {
            writer.Write(plane.Length);
            writer.Write(plane.Width);

            Write(plane.Center, writer);
            Write(plane.Normal, writer);
        }
    }
}
