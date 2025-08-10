// (c) 2017 Roland Boon

namespace BoonOrg.Functions.Logic
{
    internal sealed class ParameterTextTryParseInt : IParameterTextTryParse<int>
    {
        public bool TryParse(string text, out int variable)
        {
            return int.TryParse(text, out variable);
        }
    }
}
