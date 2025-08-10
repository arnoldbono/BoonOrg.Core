// (c) 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;

using BoonOrg.DrawingTools.ColorMapping;

namespace BoonOrg.DrawingTools.Persistence
{
    public interface IColorSetXmlReader
    {
        IEnumerable<IColorSet> Read(Stream stream);
    }
}
