// (c) 2017, 2018, 2023 Roland Boon

using System.IO;

using BoonOrg.Storage;

using BoonOrg.Geometry.Creators;
using System;

namespace BoonOrg.Geometry.Storage
{
    internal sealed class AxesStorageProvider : StorageProviderBase<IAxes>
    {
        public AxesStorageProvider(Func<IAxes> func,
            IStorageIdentifier storageIdentifier,
            IVectorCreator vectorCreator,
            IPointCreator pointCreator) : base(func, storageIdentifier, pointCreator, vectorCreator)
        {
        }

        public override void ReadContent(BinaryReader reader, IDocument document, IAxes axes)
        {
            var center = ReadPoint(reader);
            var lengths = ReadVector(reader);

            axes.SetAxes(center, lengths);
        }

        public override void WriteContent(IAxes axes, BinaryWriter writer)
        {
            Write(axes.Center, writer);
            Write(axes.Lengths, writer);
        }
    }
}
