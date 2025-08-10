// (c) 2017, 2018 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Functions
{
    /// <summary>
    /// A parameter is an item on the parameter list of a function call. 
    /// </summary>
    public interface IParameter : IIdentifiable
    {
        string Text { get; set; }

        IIdentifiable Value { get; set; }

        void Set(string name);

        void Set(string name, IIdentifiable value);

        void Set(string name, string referal);

        IIdentifiable FindValue();

        string FindText();
    }
}
