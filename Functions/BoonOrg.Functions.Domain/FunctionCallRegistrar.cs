// (c) 2017, 2018 Roland Boon

using System;
using System.Collections.Generic;

using BoonOrg.Registrations;

namespace BoonOrg.Functions.Domain
{
    internal sealed class FunctionCallRegistrar : IFunctionCallRegistrar
    {
        private readonly Dictionary<string, List<Type>> m_functionCalls = new Dictionary<string, List<Type>>();
        private readonly IResolver m_resolver;

        public FunctionCallRegistrar(IResolver resolver)
        {
            m_resolver = resolver;
        }

        public void Add<T>(string functionName) where T : class, IFunctionCall
        {
            GetOrCreate(functionName).Add(typeof(T));
        }

        public IEnumerable<IFunctionCall> Get(string functionName)
        {
            foreach (Type functionCallType in GetOrCreate(functionName))
            {
                var functionCall = m_resolver.TryResolve(functionCallType, out object funtionCallObject) ?
                    funtionCallObject as IFunctionCall :
                    Activator.CreateInstance(functionCallType) as IFunctionCall;

                if (functionCall == null)
                {
                    continue;
                }

                yield return functionCall;
            }
        }

        private List<Type> GetOrCreate(string functionName)
        {
            functionName = MapName(functionName);
            if (!m_functionCalls.TryGetValue(functionName, out List<Type> functionCallTypes))
            {
                functionCallTypes = new List<Type>();
                m_functionCalls.Add(functionName, functionCallTypes);
            }
            return functionCallTypes;
        }

        private static string MapName(string name)
        {
            return name.Replace(@"_", @"").ToLower();
        }
    }
}
