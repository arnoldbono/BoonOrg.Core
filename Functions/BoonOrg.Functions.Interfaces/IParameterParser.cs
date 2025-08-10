// (c) 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Functions
{
    /// <summary>
    /// A class to map a string to a parameter or an expression. 
    /// </summary>
    public interface IParameterParser
    {
        /// <summary>
        /// Parse a string into a parameter or an expression.
        /// </summary>
        /// <param name="text">The string to parse</param>
        /// <param name="container">The new parameter can refer to existing parameters from this list.</param>
        /// <param name="parameters">The list of parameters to add the parsed item to.</param>
        /// <param name="remainder">The remainder of the text left to parse.</param>
        /// <returns>True if this parser understood this text.</returns>
        bool Parse(string text, IIdentifiableContainer container, IParameterCollection parameters, out string remainder);

        int ParseBrackets(string text, char openBracket, char closeBracket);
    }
}
