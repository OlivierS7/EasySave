
namespace NSView {
	public class LogState: MenuState {
		public void ShowMenu(ConsoleView view) {
			//Calling controller method to open logs file
			view.Controller.OpenLogs();
			view.ChangeMenu(new StartMenuState());
		}

	}

}
