// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Storage;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Storage
{
    internal sealed class TetrahedraShapeStorageProvider : StorageProvider<ICompoundShape<ITetrahedron>>
    {
        private readonly IPointCreator m_pointCreator;
        private readonly ICompoundShapeCreator<ITetrahedron> m_tetrahedraShapeCreater;
        private readonly ITetrahedronCreator m_tetrahedronCreater;

        public TetrahedraShapeStorageProvider(Func<ICompoundShape<ITetrahedron>> func,
            IStorageIdentifier storageIdentifier,
            IPointCreator pointCreator,
            ICompoundShapeCreator<ITetrahedron> tetrahedraShapeCreater,
            ITetrahedronCreator tetrahedronCreater) : base(func, storageIdentifier)
        {
            m_pointCreator = pointCreator;
            m_tetrahedraShapeCreater = tetrahedraShapeCreater;
            m_tetrahedronCreater = tetrahedronCreater;
        }

        public override void ReadContent(BinaryReader reader, IDocument document, ICompoundShape<ITetrahedron> shape)
        {
            int count = reader.ReadInt32();

            var vertices = new List<IPoint>
            {
                m_pointCreator.Create(),
                m_pointCreator.Create(),
                m_pointCreator.Create(),
                m_pointCreator.Create()
            };

            var tetrahedra = new List<ITetrahedron>();
            for (int i = 0; i < count; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    IPoint c = vertices[j];
                    c.X = reader.ReadDouble();
                    c.Y = reader.ReadDouble();
                    c.Z = reader.ReadDouble();
                }
                tetrahedra.Add(m_tetrahedronCreater.Create(vertices));
            }

            shape.Add(tetrahedra);
        }

        public override void WriteContent(ICompoundShape<ITetrahedron> shape, BinaryWriter writer)
        {
            var tetrahedra = shape.Containers.ToList();
            writer.Write(tetrahedra.Count);

            foreach (ITetrahedron t in tetrahedra)
            {
                int i = 0;
                foreach (IPoint c in t.Vertices)
                {
                    writer.Write(c.X);
                    writer.Write(c.Y);
                    writer.Write(c.Z);
                    ++i;
                }
                System.Diagnostics.Debug.Assert(i == 4);
            }
        }
    }
}
