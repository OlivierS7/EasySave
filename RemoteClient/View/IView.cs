using RemoteClient.NSController;

namespace RemoteClient.NSView
{
    public interface IView
    {
        void Start();
        void PrintMessage(string message, int type);
        public Controller Controller { get; set; }
    }
}
