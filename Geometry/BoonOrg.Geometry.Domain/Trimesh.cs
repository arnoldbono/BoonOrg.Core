// (c) 2017, 2023 Roland Boon

using System;

using BoonOrg.Identification;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class Trimesh : TriangleContainer, ITrimesh
    {
        private readonly IPropertyContainer m_propertyContainer = new PropertyContainer();

        public Trimesh(IIdentity identity, Func<IBoundingBox> boundaryBoxFunc) : base(identity, boundaryBoxFunc)
        {
        }

        public string Material { get; set; }

        public string Texture { get; set; }

        public IPropertyContainer PropertyContainer => m_propertyContainer;

        public override void Clear()
        {
            base.Clear();

            foreach (var property in PropertyContainer.Properties)
            {
                property.Clear();
            }

            PropertyContainer.Clear();
        }

        public void GetIndicesRange(out int min, out int max)
        {
            max = int.MinValue;
            min = int.MaxValue;

            foreach (var index in m_triangleVertexIndices)
            {
                if (index < min)
                {
                    min = index;
                }

                if (index > max)
                {
                    max = index;
                }
            }
        }
    }
}
