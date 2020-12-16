using System.Windows.Forms;
using RemoteClient.NSController;

namespace RemoteClient.NSView
{
    class GraphicalView : IView
    {
        public static Controller controller;
        private StartMenu startMenu;

        /* Constructor */
        public GraphicalView(Controller controller)
        {
            this.Controller = controller;
        }

        public Controller Controller { get => controller; set => controller = value; }

        /* Method to print a message */
        public void PrintMessage(string message, int type)
        {
            startMenu.PrintMessage(message, type);
        }

        /* Method to initialize view */
        public void Start()
        {
            startMenu = new StartMenu();
            Application.Run(startMenu);
        }
    }
}
