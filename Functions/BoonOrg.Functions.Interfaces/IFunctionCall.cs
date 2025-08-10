// (c) 2017, 2018 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Functions
{
    public interface IFunctionCall
    {
        IParameterCollection Parameters { get; }

        bool Execute(IIdentifiableContainer container);
    }
}
