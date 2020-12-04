using NSView;
using NSController;
using System.Windows.Forms;

namespace NSView
{
    class GraphicalView : IView
    {
        private Controller controller;
        private StartMenu form;

        public GraphicalView(Controller controller)
        {
            this.Controller = controller;
        }

        public Controller Controller { get => controller; set => controller = value; }

        public void PrintMessage(string message, int type)
        {
            form.PrintMessage(message, type);
        }

        public void Start()
        {
            form = new StartMenu(this);
            Application.Run(form);
        }
    }
}
