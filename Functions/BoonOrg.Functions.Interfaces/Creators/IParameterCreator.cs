// (c) 2016, 2017 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Registrations;

namespace BoonOrg.Functions.Creators
{
    public interface IParameterCreator : ICreator<IParameter>
    {
        IParameter Create(string name);

        IParameter Create(string name, string text);

        IParameter Create(string name, IIdentifiable identifiable);
    }
}
