// (c) 2017 Roland Boon

namespace BoonOrg.Functions.Logic
{
    internal sealed class ParameterTextTryParseBool : IParameterTextTryParse<bool>
    {
        public bool TryParse(string text, out bool variable)
        {
            return bool.TryParse(text, out variable);
        }
    }
}
