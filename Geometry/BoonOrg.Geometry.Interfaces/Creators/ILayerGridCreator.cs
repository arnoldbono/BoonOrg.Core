// (c) 2017 Roland Boon

using System.Collections.Generic;

using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Creators
{
    public interface ILayerGridCreator : ICreator<ILayerGrid>
    {
        ILayerGrid Create(int rows, int columns, string name);

        ILayerGrid Create(List<IPoint[]> rows, string name);
    }
}
