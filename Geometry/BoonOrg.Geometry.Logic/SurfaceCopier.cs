// (c) 2015, 2017, 2019 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class SurfaceCopier : ISurfaceCopier
    {
        private readonly IEnumerable<ISurfaceCopy> m_copiers;

        public SurfaceCopier(IEnumerable<ISurfaceCopy> copiers)
        {
            m_copiers = copiers;
        }

        /// <inheritdoc/>
        public ISurface Execute(ISurface surface, string name)
        {
            foreach (ISurfaceCopy copier in m_copiers)
            {
                if (copier.Supports(surface))
                {
                    return copier.Execute(surface, name);
                }
            }

            return null;
        }
    }
}
