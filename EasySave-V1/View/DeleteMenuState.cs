using System;

namespace NSView {
	public class DeleteMenuState : MenuState  {
		public void ShowMenu(ConsoleView view) {
			Console.WriteLine("Which save template would you like to delete ?");
			//afficher les templates
			int choice;
			string input = Console.ReadLine();
			Int32.TryParse(input, out choice);
			view.Controller.DeleteSaveTemplate(choice);
			view.ChangeMenu(new ConfigurationMenuState());
		}

	}

}
