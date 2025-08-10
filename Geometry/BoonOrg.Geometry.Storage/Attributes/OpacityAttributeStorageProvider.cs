// (c) 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Storage.Attributes
{
    internal class OpacityAttributeStorageProvider : AttributeStorageProvider<IOpacityAttribute>
    {
        public override void Read(BinaryReader reader, IOpacityAttribute geometryAttribute)
        {
            geometryAttribute.Opacity = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer, IOpacityAttribute geometryAttribute)
        {
            writer.Write(geometryAttribute.Opacity);
        }
    }
}
