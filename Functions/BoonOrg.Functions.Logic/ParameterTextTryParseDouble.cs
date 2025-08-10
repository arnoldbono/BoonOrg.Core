// (c) 2017 Roland Boon

namespace BoonOrg.Functions.Logic
{
    internal sealed class ParameterTextTryParseDouble : IParameterTextTryParse<double>
    {
        public bool TryParse(string text, out double variable)
        {
            return double.TryParse(text, out variable);
        }
    }
}
