// (c) 2017 Roland Boon

namespace BoonOrg.Commands
{
    public interface IMessage
    {
        string Text { get; }
    }

    public interface IMessageHandler
    {
        void SendResult(string result);

        void SendError(string error);
    }

    public delegate void ResultMessageEventHandler(IMessage message);

    public delegate void ErrorMessageEventHandler(IMessage message);
}
