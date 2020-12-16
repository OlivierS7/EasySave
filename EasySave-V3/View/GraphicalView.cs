using NSView;
using NSController;
using System.Windows.Forms;

namespace NSView
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

        /* Print a message */
        public void PrintMessage(string message, int type)
        {
            startMenu.PrintMessage(message, type);
        }

        /* Method to initialize views */
        public void Start()
        {
            startMenu = new StartMenu();
            Application.Run(startMenu);
        }
    }
}
