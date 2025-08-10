// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Functions
{
    public interface IFunctionCallRegistrar
    {
        void Add<T>(string functionName) where T : class, IFunctionCall;

        IEnumerable<IFunctionCall> Get(string functionName);
    }
}
