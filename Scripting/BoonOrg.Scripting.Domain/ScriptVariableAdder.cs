// (c) 2017, 2023 Roland Boon

using System;

using BoonOrg.Identification;
using BoonOrg.Commands;
using BoonOrg.Functions;
using BoonOrg.Functions.Creators;
using BoonOrg.Scripting.Operations;

namespace BoonOrg.Scripting.Domain
{
    internal sealed class ScriptVariableAdder : IScriptCommandHandler
    {
        private readonly IParameterCreator m_parameterCreator;
        private readonly Func<IOperationCreateVariable> m_operationCreateVariableFunc;

        public ScriptVariableAdder(IParameterCreator parameterCreator, Func<IOperationCreateVariable> operationCreateVariableFunc)
        {
            m_parameterCreator = parameterCreator;
            m_operationCreateVariableFunc = operationCreateVariableFunc;
        }

        public string[] Keywords => new string[] { @"set", @"var" };

        public bool Parse(IIdentifiableContainer container, IMessageHandler messageHandler, IScriptCommand command)
        {
            var parameters = command.Text;
            var index = parameters.IndexOf('=');
            if (index < 0)
            {
                return false;
            }

            var variableName = parameters.Substring(0, index).Trim();
            var variableExpression = parameters.Substring(index + 1).Trim();
            if (string.IsNullOrEmpty(variableExpression))
            {
                return false;
            }

            var variable = container.Find(variableName);

            if (variable is not IParameter parameter)
            {
                parameter = m_parameterCreator.Create(variableName, variableExpression);
            }

            // Format of a parameter: "[var|set] name = value;"
            var operation = m_operationCreateVariableFunc();
            operation.Parameters.Add(parameter);

            container.Add(parameter); // make sure expressions can reference the parameter

            command.AddFunctionCall(operation);
            return true;
        }
    }
}
