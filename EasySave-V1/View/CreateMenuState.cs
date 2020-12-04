using System;
using EasySave_V1.Properties;

namespace NSView {
	public class CreateMenuState : MenuState  {
		public void ShowMenu(ConsoleView view) {
            if (view.Controller.maxTemplatesReached())
            {
				Console.WriteLine("\n  "+Resources.MaxReached);
				Console.WriteLine("\n  "+Resources.PressKeyToContinue);
				Console.ReadKey();
            }
            else
            {
				Console.WriteLine("\n===========================================");
				Console.WriteLine(" "+Resources.SaveName);
				string name = (Console.ReadLine());
				Console.WriteLine("\n===========================================");
				Console.WriteLine(" "+Resources.SourcePath);
				string srcDir = (Console.ReadLine());
				Console.WriteLine("\n===========================================");
				Console.WriteLine(" "+Resources.DestPath);
				string destDir = (Console.ReadLine());
				Console.WriteLine("\n===========================================");
				Console.WriteLine(" "+Resources.SaveType);
				Console.WriteLine(" "+Resources.SaveTypeChoice);
				int type;
				Int32.TryParse(Console.ReadLine(), out type);
				view.Controller.CreateSaveTemplate(name, srcDir, destDir, type);
			}
			view.ChangeMenu(new ConfigurationMenuState());
		}
	}
}
