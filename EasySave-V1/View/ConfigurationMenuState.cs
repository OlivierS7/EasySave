using System;
using System.Threading;

namespace NSView {
	public class ConfigurationMenuState : MenuState  {
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
			Console.WriteLine("Please choose an option:");
			Console.WriteLine("   1)Create a save template");
			Console.WriteLine("   2)Delete a save template");
			Console.WriteLine("   3)Modify existing save template");
			Console.WriteLine("   4)Back to menu");
			Console.WriteLine("   5)Exit application");
			ConsoleKeyInfo input = Console.ReadKey();
			int choice = view.CheckInput(input);
			/* Verify user's input */
			while (choice == 0 || choice > 5)
			{
				Console.WriteLine("\n===========================================");
				Console.WriteLine("/!\\Please choose a valid option:");
				Console.WriteLine("   1)Create a save template");
				Console.WriteLine("   2)Delete a save template");
				Console.WriteLine("   3)Modify existing save template");
				Console.WriteLine("   4)Back to menu");
				Console.WriteLine("   5)Exit application");
				input = Console.ReadKey();
				choice = view.CheckInput(input);
			}
			/* Change Menu in terms of user's input */
			switch (choice)
			{
				case 1:
					view.ChangeMenu(new CreateMenuState());
					break;
				case 2:
					view.ChangeMenu(new DeleteMenuState());
					break;
				case 3:
					Console.WriteLine("Not yet implemented... Back to menu");
					Thread.Sleep(2000);
					view.ChangeMenu(new StartMenuState());
					break;
				case 4:
					view.ChangeMenu(new StartMenuState());
					break;
				case 5:
					Console.Clear();
					Console.WriteLine("\n\n\n\n\n\n		   _____                                                                             _ ");
					Console.WriteLine("		  / ____|                                                                           | |");
					Console.WriteLine("		 | (___     ___    ___     _   _    ___    _   _     ___    ___     ___    _ __     | |");
					Console.WriteLine("		  \\___ \\   / _ \\  / _ \\   | | | |  / _ \\  | | | |   / __|  / _ \\   / _ \\  | '_ \\    | |");
					Console.WriteLine("		  ____) | |  __/ |  __/   | |_| | | (_) | | |_| |   \\__ \\ | (_) | | (_) | | | | |   |_|");
					Console.WriteLine("		 |_____/   \\___|  \\___|    \\__, |  \\___/   \\__,_|   |___/  \\___/   \\___/  |_| |_|   (_)");
					Console.WriteLine("		                            __/ |                                                      ");
					Console.WriteLine("		                           |___/                                                       ");
					Console.ReadKey();
					Environment.Exit(1);
					break;
			}
		}
	}
}
