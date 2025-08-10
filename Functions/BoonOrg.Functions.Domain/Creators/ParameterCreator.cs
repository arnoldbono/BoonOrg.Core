// (c) 2017, 2023 Roland Boon

using System;

using BoonOrg.Identification;

using BoonOrg.Functions.Creators;

namespace BoonOrg.Functions.Domain.Creators
{
    internal sealed class ParameterCreator : IParameterCreator
    {
        private readonly Func<IParameter> m_parameterFunc;

        public ParameterCreator(Func<IParameter> parameterFunc)
        {
            m_parameterFunc = parameterFunc;
        }

        public IParameter Create()
        {
            return Create(@"invalid");
        }

        public IParameter Create(string name)
        {
            var parameter = m_parameterFunc();
            parameter.Set(name);
            return parameter;
        }

        public IParameter Create(string name, string text)
        {
            var parameter = Create(name);
            parameter.Text = text;
            return parameter;

        }

        public IParameter Create(string name, IIdentifiable identifiable)
        {
            var parameter = Create(name);
            parameter.Value = identifiable;
            return parameter;
        }
    }
}
