// (c) 2017, 2022, 2023 Roland Boon

using System;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Geometry.Generators;

namespace BoonOrg.Geometry.Domain.Generators
{
    internal sealed class MeshGeometryService : IMeshGeometryService
    {
        private readonly IMeshGeometryGenerator[] m_generators;

        public MeshGeometryService(IEnumerable<IMeshGeometryGenerator> meshGeometryGenerators)
        {
            m_generators = meshGeometryGenerators.ToArray();
        }

        public IEnumerable<IMeshGeometry> Generate(IGeometry geometry)
        {
            Type type = geometry.GetType();
            foreach (IMeshGeometryGenerator generator in m_generators)
            {
                if (generator.HandlesType(type))
                {
                    return generator.Generate(geometry);
                }
            }
            return null;
        }

        public bool HandlesType(Type type)
        {
            return m_generators.Any(p => p.HandlesType(type));
        }
    }
}
