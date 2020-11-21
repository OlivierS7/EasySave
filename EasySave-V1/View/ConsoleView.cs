using System;
using NSController;

namespace NSView {
	public class ConsoleView : IView  {
		private MenuState currentMenu;
		private Controller controller;
		public ConsoleView(Controller controller) {
			this.controller = controller;
			this.currentMenu = new StartMenuState();
			this.currentMenu.ShowMenu(this, controller);
	}

        public Controller Controller { get => controller; }

        public void ChangeMenu(MenuState state, Controller controller) {
			this.currentMenu = state;
			this.currentMenu.ShowMenu(this, controller);
		}
	}
}
