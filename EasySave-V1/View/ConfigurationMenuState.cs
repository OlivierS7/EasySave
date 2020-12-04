using System;
using System.Threading;
using EasySave_V1.Properties;

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
			Console.WriteLine("  "+Resources.Option);
			Console.WriteLine("  _________________________________________________________________________________");
			Console.WriteLine(" |");
			Console.WriteLine(" | 1 - " + Resources.CreateSave);
			Console.WriteLine(" | 2 - " + Resources.DeleteSave);
			Console.WriteLine(" | 3 - " + Resources.ModifySave);
			Console.WriteLine(" | 4 - " + Resources.BackToMenu);
			Console.WriteLine(" | 5 - " + Resources.Exit);
			Console.WriteLine(" |_________________________________________________________________________________");
			ConsoleKeyInfo input = Console.ReadKey();
			int choice = view.CheckInput(input);
			/* Verify user's input */
			while (choice == 0 || choice > 5)
			{
				Console.WriteLine("\n===========================================");
				Console.WriteLine(@"    /!\"+Resources.InvalidChoice+@"/!\");
				Console.WriteLine("  _________________________________________________________________________________");
				Console.WriteLine(" |");
				Console.WriteLine(" | 1 - " + Resources.CreateSave);
				Console.WriteLine(" | 2 - " + Resources.DeleteSave);
				Console.WriteLine(" | 3 - " + Resources.ModifySave);
				Console.WriteLine(" | 4 - " + Resources.BackToMenu);
				Console.WriteLine(" | 5 - " + Resources.Exit);
				Console.WriteLine(" |_________________________________________________________________________________");
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
					view.ChangeMenu(new ModifyMenuState());
					break;
				case 4:
					view.ChangeMenu(new StartMenuState());
					break;
				case 5:
					Console.Clear();
					Console.WriteLine("\n\n\n\n\n\n		   _____                                                                             _ ");
					Console.WriteLine("		  / ____|                                                                           | |");
					Console.WriteLine("		 | (___     ___    ___     _   _    ___    _   _     ___    ___     ___    _ __     | |");
					Console.WriteLine(@"		  \___ \   / _ \  / _ \   | | | |  / _ \  | | | |   / __|  / _ \   / _ \  | '_ \    | |");
					Console.WriteLine(@"		  ____) | |  __/ |  __/   | |_| | | (_) | | |_| |   \__ \ | (_) | | (_) | | | | |   |_|");
					Console.WriteLine(@"		 |_____/   \___|  \___|    \__, |  \___/   \__,_|   |___/  \___/   \___/  |_| |_|   (_)");
					Console.WriteLine("		                            __/ |                                                      ");
					Console.WriteLine("		                           |___/                                                       ");
					Console.ReadKey();
					Environment.Exit(1);
					break;
			}
		}
	}
}
