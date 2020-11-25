using NSView;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NSView
{
    class ModifyMenuState : MenuState
	{
		public void ShowMenu(ConsoleView view)
		{
			try
			{
				Console.WriteLine("\n  Which save template would you like to modify ?");
				List<string> templatesName = view.Controller.GetAllTemplates();
				for (int i = 0; i < templatesName.Count; i++)
				{
					Console.WriteLine("    " + (i + 1) + " - " + templatesName[i]);
				}
				int choice;
				string input = Console.ReadLine();
				Int32.TryParse(input, out choice);
				if(choice == 0 || choice > view.Controller.GetAllTemplates().Count)
                {
					Console.WriteLine("\nNot a valid choice, going back to selection...");
					Thread.Sleep(2000);
					view.ChangeMenu(new StartMenuState());
				}
                else
                {
					Console.WriteLine("\n===========================================");
					Console.WriteLine(" Please enter destination directory path:");
					string destDir = (Console.ReadLine());
					Console.WriteLine("\n===========================================");
					Console.WriteLine(" Please pick a save type for the template:");
					Console.WriteLine(" 1 - Full backup | 2 - Differential backup");
					int type;
					Int32.TryParse(Console.ReadLine(), out type);
					view.Controller.ModifySaveTemplate(choice, destDir, type);
				}
			}
			catch (Exception err)
			{
				view.PrintMessage(err.Message);
			}
			view.ChangeMenu(new ConfigurationMenuState());
		}
	}
}
