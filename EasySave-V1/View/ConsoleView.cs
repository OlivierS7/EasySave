using System;
using System.Threading;
using NSController;

namespace NSView {
	public class ConsoleView : IView  {
		private MenuState currentMenu;
		private Controller controller;
		public ConsoleView(Controller controller) {
			this.controller = controller;
			this.CurrentMenu = new StartMenuState();
	}

        public Controller Controller { get => controller; }
        public MenuState CurrentMenu { get => currentMenu; set => currentMenu = value; }

        public void ChangeMenu(MenuState state) {
			this.CurrentMenu = state;
			this.CurrentMenu.ShowMenu(this);
		}
		public void PrintMessage (string message)
        {
			Console.WriteLine(message);
			Console.WriteLine("Press any key to continue");
			Console.ReadKey();
        }
	}
}
