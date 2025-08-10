// (c) 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Functions
{
    public interface IParameterCollection : IIdentifiableCollection<IParameter>
    {
        void AddParameter(string parameterName, IIdentifiable item);

        void AddParameter(string parameterName, string parameterValue);

        IParameter GetParameter(string name);

        T FindReference<T>(string name) where T : class, IIdentifiable;

        T FindReference<T>(IParameter parameter) where T : class, IIdentifiable;

        IIdentifiable FindReference(string name);

        bool Get<T>(IParameter parameter, out T value) where T : class, IIdentifiable;

        bool Get<T>(string name, out T value) where T : class, IIdentifiable;

        bool GetBool(string name, out bool value);

        bool GetString(string name, out string value);

        bool GetPath(string name, out string value);

        bool GetInt(string name, out int value);

        bool GetValue<T>(string name, out T value);

        bool GetValue<T>(IParameter parameter, out T value);

        bool Get<T>(string name, IIdentifiableContainer container, out T value);

        bool Get<T>(IParameter parameter, IIdentifiableContainer container, out T value);

        bool GetIdentifiable<T>(string name, IIdentifiableContainer container, out T value) where T : class, IIdentifiable;

        T GetIdentifiableOrCreate<T>(string name, IIdentifiableContainer container) where T : class, IIdentifiable;
    }
}
