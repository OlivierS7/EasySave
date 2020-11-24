using System;
using System.Collections.Generic;

namespace NSView {
	public class DeleteMenuState : MenuState  {
		public void ShowMenu(ConsoleView view) {
			try
            {
				Console.WriteLine("\nWhich save template would you like to delete ?");
				List<string> templatesName = view.Controller.GetAllTemplates();
				for (int i = 0; i < templatesName.Count; i++)
				{
					Console.WriteLine((i + 1) + " - " + templatesName[i]);
				}
				int choice;
				string input = Console.ReadLine();
				Int32.TryParse(input, out choice);
				view.Controller.DeleteSaveTemplate(choice);
			} catch (Exception err)
            {
				view.PrintMessage(err.Message);
            }
			view.ChangeMenu(new ConfigurationMenuState());
		}

	}

}
