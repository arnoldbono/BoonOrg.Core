// (c) 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Functions
{
    /// <summary>
    /// A class to map the text of a parameter to a type of identifiable. 
    /// </summary>
    public interface IParameterTextMap
    {
        /// <summary>
        /// Try to map the text of a parameter into a type of identifiable.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="container">The variable container.</param>
        /// <param name="variable">The mapped variable, or null if text cannot be mapped.</param>
        /// <returns>True if this mapper understood this text.</returns>
        bool TryMap(string text, IIdentifiableContainer container, out IIdentifiable variable);
    }

    /// <summary>
    /// Try to map one identifiable into the given type <paramref name="T"/>.
    /// </summary>
    public interface IIdentifiableMapping<T>
    {
        /// <summary>
        /// Try to map the given item to the given type <paramref name="T"/>.
        /// </summary>
        /// <param name="item">The item to map</param>
        /// <param name="container">The new parameter can refer to existing parameters from this list.</param>
        /// <param name="variable">The mapped variable, or default(T) if item does not match.</param>
        /// <returns>True iff this mapper understood the given value.</returns>
        bool TryMap(IIdentifiable item, IIdentifiableContainer container, out T variable);
    }

    public interface IParameterTextMap<T> : IIdentifiableMapping<T>, IParameterTextMap
    {
        /// <summary>
        /// Try to map the text of a parameter into a type of identifiable.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="container">The variable container.</param>
        /// <param name="variable">The mapped variable, or default(T) if text cannot be mapped.</param>
        /// <returns>True if this mapper understood this text.</returns>
        bool TryMap(string text, IIdentifiableContainer container, out T variable);
    }
}
