using System;
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
			Console.WriteLine("\n		/!\\WARNING/!\\\n" + message);
			Console.WriteLine("\nPress any key to continue...");
			Console.ReadKey();
        }
		public void Progression (string progression)
        {
			Console.WriteLine(progression);
        }
		public int CheckInput(ConsoleKeyInfo input)
        {
			if (ConsoleKey.D1 == input.Key || ConsoleKey.NumPad1 == input.Key)
				return 1;
			if (ConsoleKey.D2 == input.Key || ConsoleKey.NumPad2 == input.Key)
				return 2;
			if (ConsoleKey.D3 == input.Key || ConsoleKey.NumPad3 == input.Key)
				return 3;
			if (ConsoleKey.D4 == input.Key || ConsoleKey.NumPad4 == input.Key)
				return 4;
			if (ConsoleKey.D5 == input.Key || ConsoleKey.NumPad5 == input.Key)
				return 5;
			if (ConsoleKey.D6 == input.Key || ConsoleKey.NumPad6 == input.Key)
				return 6;
			if (ConsoleKey.D7 == input.Key || ConsoleKey.NumPad7 == input.Key)
				return 7;
			if (ConsoleKey.D8 == input.Key || ConsoleKey.NumPad8 == input.Key)
				return 8;
			if (ConsoleKey.D9 == input.Key || ConsoleKey.NumPad9 == input.Key)
				return 9;
			return 0;
		}
	}
}
