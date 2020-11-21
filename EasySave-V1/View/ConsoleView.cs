using System;
using NSController;

namespace NSView {
	public class ConsoleView : IView  {
		private MenuState currentState;
		private Controller controller;
		public ConsoleView(Controller controller) {
			this.controller = controller;
			this.currentState = new StartMenuState();
			this.currentState.ShowMenu(this, controller);
	}

        public Controller Controller { get => controller; }

        public void ChangeMenu(MenuState state, Controller controller) {
			this.currentState = state;
			this.currentState.ShowMenu(this, controller);
		}
	}
}
