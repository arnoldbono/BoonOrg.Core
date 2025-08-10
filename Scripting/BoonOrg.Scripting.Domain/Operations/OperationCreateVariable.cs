// (c) 2017, 2018 Roland Boon

using System.Collections.Generic;
using System.Linq;

using BoonOrg.Functions;
using BoonOrg.Functions.Domain;
using BoonOrg.Identification;
using BoonOrg.Scripting.Operations;

namespace BoonOrg.Scripting.Domain.Operations
{
    internal sealed class OperationCreateVariable : Operation, IOperationCreateVariable
    {
        private readonly IEnumerable<IParameterTextMap> m_mappers;

        public OperationCreateVariable(IParameterCollection parameters,
            IEnumerable<IParameterTextMap> mappers) : base(parameters)
        {
            m_mappers = mappers;
        }

        public override bool Execute(IIdentifiableContainer container)
        {
            IParameter parameter = Parameters.FirstOrDefault();
            if (parameter == null)
            {
                return false;
            }

            return CreateVariable(container, parameter.Identification.Name, parameter.Text);
        }

        private bool CreateVariable(IIdentifiableContainer container, string name, string parameters)
        {
            if (!CheckIsValidName(name) || string.IsNullOrEmpty(parameters))
            {
                return false;
            }

            var item = container.FindByName<IIdentifiable>(name);
            if (item != null)
            {
                container.Remove(item);
            }

            foreach (var mapper in m_mappers)
            {
                if (mapper.TryMap(parameters, container, out item))
                {
                    item.Identification.Rename(name);
                    container.Add(item);
                    return true;
                }
            }

            return false;
        }

        private bool CheckIsValidName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}
