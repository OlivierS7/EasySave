using System;
using System.Collections.Generic;
using System.Threading;

namespace NSView {
	public class ExecuteConfigurationState : MenuState {
		public void ShowMenu(ConsoleView view) {
			Console.Clear();
			Console.WriteLine(" __________________________________________________________________________________ ");
			Console.WriteLine("|  ______               _____  __     __     _____             __      __  ______  |");
			Console.WriteLine("| |  ____|     /\\      / ____| \\ \\   / /    / ____|     /\\     \\ \\    / / |  ____| |");
			Console.WriteLine("| | |__       /  \\    | (___    \\ \\_/ /    | (___      /  \\     \\ \\  / /  | |__    |");
			Console.WriteLine("| |  __|     / /\\ \\    \\___ \\    \\   /      \\___ \\    / /\\ \\     \\ \\/ /   |  __|   |");
			Console.WriteLine("| | |____   / ____ \\   ____) |    | |       ____) |  / ____ \\     \\  /    | |____  |");
			Console.WriteLine("| |______| /_/    \\_\\ |_____/     |_|      |_____/  /_/    \\_\\     \\/     |______| |");
			Console.WriteLine("|__________________________________________________________________________________| \n \n");
			Console.WriteLine("Do you want to execute one or multiple save template(s) ?");
			Console.WriteLine("1 - Execute one template | 2 - Execute all templates");
			Console.WriteLine("3 - Go back to menu");
			ConsoleKeyInfo input = Console.ReadKey();
			int choice = view.CheckInput(input);
			if (choice == 3)
            {
				view.ChangeMenu(new StartMenuState());
            }
            /* Verify user's input */
            try 
			{
				while (choice != 1 && choice != 2)
				{
					Console.WriteLine("\n===========================================");
					Console.WriteLine("/!\\Please enter a valid choice");
					Console.WriteLine("1 - Execute one template | 2 - Execute all templates");
					input = Console.ReadKey();
				    choice = view.CheckInput(input);
				}
				if (choice == 1)
				{
					Console.WriteLine("\nPlease choose the save template to execute :");
					List<string> templatesName = view.Controller.GetAllTemplates();
					for (int i = 0; i < templatesName.Count; i++)
					{
						Console.WriteLine((i + 1) + " - " + templatesName[i]);
					}
					string stringInput = Console.ReadLine();
					Int32.TryParse(stringInput, out choice);
					view.Controller.ExecuteOneSave(choice);
					view.ChangeMenu(new StartMenuState());
				}
				else
				{
					Console.WriteLine("Are you sure you want to execute all these templates ? (y/n)");
					List<string> templatesName = view.Controller.GetAllTemplates();
					for (int i = 0; i < templatesName.Count; i++)
					{
						Console.WriteLine((i + 1) + " - " + templatesName[i]);
					}
					/* Change Menu in terms of user's input */
					string yesNo = Console.ReadLine();
					if (yesNo == "y" || yesNo == "Y")
					{
						view.Controller.ExecuteAllSave();
						view.ChangeMenu(new StartMenuState());
					}
					else if (yesNo == "n" || yesNo == "N")
					{
						Console.WriteLine("Going back to selection...");
						Thread.Sleep(2000);
						view.ChangeMenu(new ExecuteConfigurationState());
					}
					else
					{
						Console.WriteLine("Not a valid choice, going back to selection...");
						Thread.Sleep(2000);
					}
				}
			} catch (Exception err)
            {
				view.PrintMessage(err.Message);
            }
			view.ChangeMenu(new ExecuteConfigurationState());
		}

	}

}
