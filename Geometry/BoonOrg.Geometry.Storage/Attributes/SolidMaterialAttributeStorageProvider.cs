// (c) 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Storage.Attributes
{
    internal class SolidMaterialAttributeStorageProvider : AttributeStorageProvider<ISolidMaterialAttribute>
    {
        public override void Read(BinaryReader reader, ISolidMaterialAttribute geometryAttribute)
        {
            ReadColor(reader, geometryAttribute.Front);
            ReadColor(reader, geometryAttribute.Back);
        }

        public override void Write(BinaryWriter writer, ISolidMaterialAttribute geometryAttribute)
        {
            WriteColor(geometryAttribute.Front, writer);
            WriteColor(geometryAttribute.Back, writer);
        }

        private static void WriteColor(IColor color, BinaryWriter writer)
        {
            writer.Write(color.Red);
            writer.Write(color.Green);
            writer.Write(color.Blue);
        }

        private void ReadColor(BinaryReader reader, IColor color)
        {
            color.Alpha = 255;
            color.Red = reader.ReadByte();
            color.Green = reader.ReadByte();
            color.Blue = reader.ReadByte();
        }
    }
}
