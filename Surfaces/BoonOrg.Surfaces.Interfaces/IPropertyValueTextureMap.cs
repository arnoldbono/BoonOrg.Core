// (c) 2024 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Surfaces
{
    public interface IPropertyValueTextureMap : IPropertyValue
    {
        public double U { get; }

        public double V { get; }
    }
}
