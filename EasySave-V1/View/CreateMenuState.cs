using System;

namespace NSView {
	public class CreateMenuState : MenuState  {
		public void ShowMenu(ConsoleView view) {
            if (view.Controller.maxTemplatesReached())
            {
				Console.WriteLine("\n  Maximum number of save templates reached. Please delete one before adding another");
				Console.WriteLine("\n  Press any key to continue...");
				Console.ReadKey();
            }
            else
            {
				Console.WriteLine("\n===========================================");
				Console.WriteLine(" Please enter save template name:");
				string name = (Console.ReadLine());
				Console.WriteLine("\n===========================================");
				Console.WriteLine(" Please enter source directory path:");
				string srcDir = (Console.ReadLine());
				Console.WriteLine("\n===========================================");
				Console.WriteLine(" Please enter destination directory path:");
				string destDir = (Console.ReadLine());
				Console.WriteLine("\n===========================================");
				Console.WriteLine(" Please pick a save type for the template:");
				Console.WriteLine(" 1 - Full backup | 2 - Differential backup");
				int type;
				Int32.TryParse(Console.ReadLine(), out type);
				view.Controller.CreateSaveTemplate(name, srcDir, destDir, type);
			}
			view.ChangeMenu(new ConfigurationMenuState());
		}
	}
}
