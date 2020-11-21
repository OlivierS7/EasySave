using System;
using System.Diagnostics;
using NSModel.Singleton;

namespace NSView {
	public class LogState: MenuState {
		public void ShowMenu(ConsoleView view) {
			Process.Start("Notepad.exe", Log.GetLogInstance().fileName);
			view.ChangeMenu(new StartMenuState());
		}

	}

}
