// (c) 2018, 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Functions;

namespace BoonOrg.Storage.Domain
{
    internal sealed class ParameterStorageProvider : StorageProvider<IParameter>
    {
        public ParameterStorageProvider(Func<IParameter> func, IStorageIdentifier storageIdentifier) :
            base(func, storageIdentifier)
        {
        }

        public override void ReadContent(BinaryReader reader, IDocument document, IParameter parameter)
        {
            parameter.Text = reader.ReadString();
        }

        public override void WriteContent(IParameter parameter, BinaryWriter writer)
        {
            if (parameter.Value != null)
            {
                writer.Write(parameter.Value.Identification.Reference);
                return;
            }

            writer.Write(parameter.Text);
        }
    }
}
