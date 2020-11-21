using System;
using System.Collections.Generic;
using NSController;

namespace NSView {
	public class DeleteMenuState : MenuState  {
		public void ShowMenu(ConsoleView view, Controller controller) {
			Console.WriteLine("Which save template would you like to delete ?");
			List<string> templatesName = controller.GetAllTemplates();
			for (int i = 0; i < templatesName.Count; i++)
			{
				Console.WriteLine((i+1) + " - " + templatesName[i]);
			}
			int choice;
			string input = Console.ReadLine();
			Int32.TryParse(input, out choice);
			view.Controller.DeleteSaveTemplate(choice);
			view.ChangeMenu(new ConfigurationMenuState(), controller);
		}

	}

}
