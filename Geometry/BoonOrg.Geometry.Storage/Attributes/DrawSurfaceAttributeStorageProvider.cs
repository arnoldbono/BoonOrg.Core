// (c) 2023 Roland Boon

using System;
using System.IO;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Storage.Attributes
{
    internal class DrawSurfaceAttributeStorageProvider : AttributeStorageProvider<IDrawSurfaceAttribute>
    {
        public override void Read(BinaryReader reader, IDrawSurfaceAttribute geometryAttribute)
        {
            geometryAttribute.ShowSurface = reader.ReadBoolean();
            geometryAttribute.ShowWireframe = reader.ReadBoolean();
            geometryAttribute.ShowProperty = reader.ReadBoolean();
        }

        public override void Write(BinaryWriter writer, IDrawSurfaceAttribute geometryAttribute)
        {
            writer.Write(geometryAttribute.ShowSurface);
            writer.Write(geometryAttribute.ShowWireframe);
            writer.Write(geometryAttribute.ShowProperty);
        }
    }
}
