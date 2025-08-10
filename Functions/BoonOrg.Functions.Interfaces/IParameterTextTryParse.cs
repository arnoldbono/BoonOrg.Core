// (c) 2017 Roland Boon

namespace BoonOrg.Functions
{
    /// <summary>
    /// A class to map the text of a parameter to a type. 
    /// </summary>
    public interface IParameterTextTryParse<T>
    {
        /// <summary>
        /// Try to parse the text of a parameter into the given type <paramref name="T"/>.
        /// </summary>
        /// <param name="text">The string to parse</param>
        /// <param name="variable">The parsed variable, or default(T) if text cannot be parsed.</param>
        /// <returns>True if this parser understood this text.</returns>
        bool TryParse(string text, out T variable);
    }
}
