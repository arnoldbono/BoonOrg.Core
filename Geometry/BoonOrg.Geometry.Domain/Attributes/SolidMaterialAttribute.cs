// (c) 2023 Roland Boon

using System;

using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Domain.Attributes
{
    public class SolidMaterialAttribute : ISolidMaterialAttribute
    {
        public SolidMaterialAttribute(Func<IColor> colorFunc)
        {
            Front = colorFunc();
            Back = colorFunc();
        }

        public IColor Front { get; set; }

        public IColor Back { get; set; }
    }
}
