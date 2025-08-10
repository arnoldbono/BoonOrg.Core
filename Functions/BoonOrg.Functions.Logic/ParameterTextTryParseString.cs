// (c) 2017 Roland Boon

namespace BoonOrg.Functions.Logic
{
    internal sealed class ParameterTextTryParseString : IParameterTextTryParse<string>
    {
        public bool TryParse(string text, out string variable)
        {
            variable = text;
            return !string.IsNullOrEmpty(text);
        }
    }
}
