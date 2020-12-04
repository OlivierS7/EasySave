using System;
using System.Collections.Generic;
using EasySave_V1.Properties;

namespace NSView {
	public class DeleteMenuState : MenuState  {
		public void ShowMenu(ConsoleView view) {
			try
            {
				Console.WriteLine("\n  "+Resources.SelecDel);
				List<string> templatesName = view.Controller.GetAllTemplates();
				for (int i = 0; i < templatesName.Count; i++)
				{
					Console.WriteLine("    " + (i + 1) + " - " + templatesName[i]);
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
