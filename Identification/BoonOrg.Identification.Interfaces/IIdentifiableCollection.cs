// (c) 2017, 2018 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Identification
{
    public interface IIdentifiableCollection<T> : IIdentifiable, ICollection<T> where T : class, IIdentifiable
    {
        T FindReferal(string referal);

        T Find(string name);
    }
}
