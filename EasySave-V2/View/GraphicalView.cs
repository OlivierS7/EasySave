using NSView;
using NSController;
using System.Windows.Forms;

namespace NSView
{
    class GraphicalView : IView
    {
        private Controller controller;
        private StartMenu startMenu;

        public GraphicalView(Controller controller)
        {
            this.Controller = controller;
        }

        public Controller Controller { get => controller; set => controller = value; }

        public void PrintMessage(string message, int type)
        {
            startMenu.PrintMessage(message, type);
        }

        public void Start()
        {
            startMenu = new StartMenu(this);
            Application.Run(startMenu);
        }
    }
}
