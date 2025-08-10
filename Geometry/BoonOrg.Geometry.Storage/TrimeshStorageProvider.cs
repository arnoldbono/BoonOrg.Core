// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Storage;

namespace BoonOrg.Geometry.Storage
{
    internal sealed class TrimeshStorageProvider : StorageProvider<ITrimesh>
    {
        private readonly Func<IPoint> m_pointFunc;

        public TrimeshStorageProvider(Func<ITrimesh> func,
            IStorageIdentifier storageIdentifier,
            Func<IPoint> pointFunc) : base(func, storageIdentifier)
        {
            m_pointFunc = pointFunc;
        }

        public override void ReadContent(BinaryReader reader, IDocument document, ITrimesh trimesh)
        {
            var count = reader.ReadInt32();
            var vertexIndices = new List<int>(count);
            for (int i = 0; i < count; ++i)
            {
                vertexIndices.Add(reader.ReadInt32());
            }

            count = reader.ReadInt32();
            var vertices = new List<IPoint>(count);
            for (int i = 0; i < count; ++i)
            {
                var c = m_pointFunc();
                c.X = reader.ReadDouble();
                c.Y = reader.ReadDouble();
                c.Z = reader.ReadDouble();
                vertices.Add(c);
            }

            trimesh.Add(vertexIndices, vertices);
        }

        public override void WriteContent(ITrimesh trimesh, BinaryWriter writer)
        {
            var vertexIndices = trimesh.TriangleVertexIndices.ToList();
            var vertices = trimesh.Vertices.ToList();

            var count = vertexIndices.Count;
            writer.Write(count);
            for (int i = 0; i < count; ++i)
            {
                writer.Write(vertexIndices[i]);
            }

            count = vertices.Count;
            writer.Write(count);
            for (int i = 0; i < count; ++i)
            {
                var c = vertices[i];
                writer.Write(c.X);
                writer.Write(c.Y);
                writer.Write(c.Z);
            }
        }
    }
}
