using System;
using System.Diagnostics;
using NSModel.Singleton;
using NSController;


namespace NSView {
	public class LogState: MenuState {
		public void ShowMenu(ConsoleView view, Controller controller) {
			Process.Start("Notepad.exe", Log.GetLogInstance().fileName);
			view.ChangeMenu(new StartMenuState(), controller);
		}

	}

}
