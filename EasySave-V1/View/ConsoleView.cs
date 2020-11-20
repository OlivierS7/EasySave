using System;
using NSController;

namespace NSView {
	public class ConsoleView : IView  {
		public ConsoleView(Controller controller) {
			Console.WriteLine("Veuillez entrer le nom de la sauvegarde :");
			string name = (Console.ReadLine());
			Console.WriteLine("Veuillez entrer le nom du dossier source :");
			string srcDir = (Console.ReadLine());
			Console.WriteLine("Veuillez entrer le nom du dossier de destination :");
			string destDir = (Console.ReadLine());
			Console.WriteLine("1 - Backup complète\n2 - Backup incrémentielle");
			int type = (int.Parse(Console.ReadLine()));
			controller.CreateSaveTemplate(name, srcDir, destDir, type);
			//throw new System.NotImplementedException("Not implemented");
		}
		public void ChangeMenu(MenuState state) {
			throw new System.NotImplementedException("Not implemented");
		}

		private MenuState AChanger;

		//private Program program;

	}

}
