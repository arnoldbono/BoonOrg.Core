// (c) 2017, 2018 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Functions;
using BoonOrg.Commands;

namespace BoonOrg.Scripting.Domain
{
    internal sealed class ScriptFunctionParser : IScriptCommandHandler
    {
        private readonly IParameterParser m_parameterParser;
        private readonly IFunctionCallRegistrar m_registrar;

        public ScriptFunctionParser(IParameterParser parameterParser,
            IFunctionCallRegistrar registrar)
        {
            m_parameterParser = parameterParser;
            m_registrar = registrar;
        }

        public string[] Keywords => new string[] { @"call" };

        public bool Parse(IIdentifiableContainer container, IMessageHandler messageHandler, IScriptCommand command)
        {
            string parameters = command.Text;

            int indexOpen = parameters.IndexOf('(');
            if (indexOpen < 0)
            {
                return false;
            }

            int indexClose = m_parameterParser.ParseBrackets(parameters, '(', ')') - 1;
            if (indexClose < indexOpen)
            {
                return false;
            }

            string functionName = parameters.Substring(0, indexOpen).Trim();

            var functionCalls = m_registrar.Get(functionName);

            string functionParameters = parameters.Substring(indexOpen + 1, indexClose - indexOpen - 1).Trim();
            foreach (IFunctionCall functionCall in functionCalls)
            {
                if (!ProcessParameterList(functionCall, functionParameters, container))
                {
                    messageHandler.SendError($@"Failed to parse: ""{parameters}""");
                    return false;
                }

                command.AddFunctionCall(functionCall);
            }

            return true;
        }

        private bool ProcessParameterList(IFunctionCall functionCall, string functionParameters, IIdentifiableContainer container)
        {
            IParameterCollection parameters = functionCall.Parameters;

            while (!string.IsNullOrEmpty(functionParameters))
            {
                if (!m_parameterParser.Parse(functionParameters, container, parameters, out string remainder))
                {
                    return false;
                }

                functionParameters = remainder;
            }

            return true;
        }
    }
}
