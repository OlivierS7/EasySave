using System;
using System.Collections.Generic;
using System.Threading;
using NSController;


namespace NSView {
	public class ExecuteConfigurationState : MenuState {
		public void ShowMenu(ConsoleView view, Controller controller) {
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
			Console.WriteLine("3)Go back to menu");
			int choice;
			string input = Console.ReadLine();
			Int32.TryParse(input, out choice);
			if (choice == 3)
            {
				view.ChangeMenu(new StartMenuState(), controller);
            }
			while (choice != 1 && choice != 2)
            {
				Console.WriteLine("===========================================");
				Console.WriteLine("/!\\Please enter a valid choice");
				Console.WriteLine("1 - Execute one template | 2 - Execute all templates");
				input = Console.ReadLine();
				Int32.TryParse(input, out choice);
			}
			//Afficher la liste ici
			if(choice == 1)
            {
				Console.WriteLine("Please choose the save template to execute :");
				List<string> templatesName = controller.GetAllTemplates();
				for (int i = 0; i < templatesName.Count; i++)
				{
					Console.WriteLine((i + 1) + " - " + templatesName[i]);
				}
				input = Console.ReadLine();
				Int32.TryParse(input, out choice);
				view.Controller.ExecuteSave(choice);
				view.ChangeMenu(new StartMenuState(), controller);
            } else
            {
				Console.WriteLine("Are you sure you want to execute all these templates ? (y/n)");
				List<string> templatesName = controller.GetAllTemplates();
				for (int i = 0; i < templatesName.Count; i++)
				{
					Console.WriteLine((i + 1) + " - " + templatesName[i]);
				}
				string yesNo = Console.ReadLine();
				if (yesNo == "y" || yesNo == "Y")
                {
					view.Controller.ExecuteAllSave();
					view.ChangeMenu(new StartMenuState(), controller);
				} else if (yesNo == "n" || yesNo == "N")
                {
					Console.WriteLine("Going back to selection...");
					Thread.Sleep(2000);
					view.ChangeMenu(new ExecuteConfigurationState(), controller);
				} else
                {
					Console.WriteLine("Not a valid choice, going back to selection...");
					Thread.Sleep(2000);
					view.ChangeMenu(new ExecuteConfigurationState(), controller);
                }
            }
		}

	}

}
