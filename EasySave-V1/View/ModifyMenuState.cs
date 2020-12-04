using NSView;
using System;
using System.Collections.Generic;
using System.Threading;
using EasySave_V1.Properties;

namespace NSView
{
    class ModifyMenuState : MenuState
	{
		public void ShowMenu(ConsoleView view)
		{
			try
			{
				Console.WriteLine("\n  "+Resources.SaveToModif);
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
					Console.WriteLine("\n"+Resources.InvalidToSelec);
					Thread.Sleep(2000);
					view.ChangeMenu(new StartMenuState());
				}
                else
                {
					Console.WriteLine("\n===========================================");
					Console.WriteLine(" "+Resources.DestPath);
					string destDir = (Console.ReadLine());
					Console.WriteLine("\n===========================================");
					Console.WriteLine(" "+Resources.SaveType);
					Console.WriteLine(" "+Resources.SaveTypeChoice);
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
