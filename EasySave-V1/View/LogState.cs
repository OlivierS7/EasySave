using NSController;


namespace NSView {
	public class LogState: MenuState {
		public void ShowMenu(ConsoleView view, Controller controller) {
			//Calling controller method to open logs file
			view.Controller.OpenLogs();
			view.ChangeMenu(new StartMenuState(), controller);
		}

	}

}
