// (c) 2017, 2018 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Functions.Creators;

namespace BoonOrg.Functions.Domain
{
    internal sealed class ParameterParser : IParameterParser
    {
        private readonly IParameterCreator m_parameterCreator;

        public ParameterParser(IParameterCreator parameterCreator)
        {
            m_parameterCreator = parameterCreator;
        }

        public bool Parse(string text, IIdentifiableContainer container, IParameterCollection parameters, out string remainder)
        {
            remainder = string.Empty;

            text = text.TrimStart();

            int indexEqualSign = text.IndexOf('=');
            if (indexEqualSign < 0)
            {
                return false;
            }

            string parameterName = text.Substring(0, indexEqualSign).Trim();
            string parameterValue = text.Substring(indexEqualSign + 1).Trim();

            int startIndex = 0;
            if (parameterValue.StartsWith(@"["))
            {
                startIndex = ParseBrackets(parameterValue, '[', ']');
            }
            else if (parameterValue.StartsWith(@"{"))
            {
                startIndex = ParseBrackets(parameterValue, '{', '}');
            }

            if (startIndex < 0)
            {
                return false;
            }

            SplitOffParameterAndGetRemainder(ref parameterValue, startIndex, out remainder);

            var reference = container.FindByName<IIdentifiable>(parameterValue);
            if (reference is IParameter)
            {
                reference = null;
            }

            IParameter parameter = (reference != null) ?
                m_parameterCreator.Create(parameterName, reference) :
                m_parameterCreator.Create(parameterName, parameterValue);

            if (parameters != null)
            {
                parameters.Add(parameter);
                return true;
            }

            container.Add(parameter);
            return true;
        }

        public int ParseBrackets(string text, char openBracket, char closeBracket)
        {
            int countOpenBracket = 0;
            int countCloseBracket = 0;
            int length = text.Length;
            for (var i = 0; i < length; ++i)
            {
                char ch = text[i];
                if (ch == openBracket)
                {
                    ++countOpenBracket;
                }
                else if (ch == closeBracket)
                {
                    ++countCloseBracket;
                }

                if (countOpenBracket == 0)
                {
                    continue;
                }

                if (countOpenBracket == countCloseBracket)
                {
                    return i + 1;
                }
            }
            return -1;
        }

        private void SplitOffParameterAndGetRemainder(ref string text, int startIndex, out string remainder)
        {
            int indexComma = text.IndexOf(',', startIndex);
            remainder = (indexComma >= 0 && indexComma + 1 < text.Length)
                ? text.Substring(indexComma + 1).TrimStart()
                : string.Empty;
            if (indexComma >= 0)
            {
                text = text.Substring(0, indexComma).TrimEnd();
            }
        }
    }
}
