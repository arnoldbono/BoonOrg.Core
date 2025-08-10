// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;

namespace BoonOrg.Geometry.Domain.Attributes
{
    public class OpacityAttribute : IOpacityAttribute
    {
        public OpacityAttribute()
        {
            Opacity = 255;
        }

        public byte Opacity { get; set; }
    }
}
