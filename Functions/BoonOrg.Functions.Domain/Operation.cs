// (c) 2016, 2017, 2023 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Functions.Domain
{
    public class Operation : IOperation
    {
        public IParameterCollection Parameters { get; }

        public Operation(IParameterCollection parameters)
        {
            Parameters = parameters;
        }

        public virtual bool Execute(IIdentifiableContainer container) => true;
    }
}
