using NSModel;
using NSView;


namespace NSController {
	public class Controller {

		private ConsoleView _consoleView;
		private IView _View;
		private Model _model;

		public ConsoleView consoleView
		{
			get => this._consoleView;
			set => this._consoleView = value;
		}

		public IView View
		{
			get => this._View;
			set => this._View = value;
		}

		public Model model
		{
			get => this._model;
			set => this._model = value;
		}

		public Controller() {
			this.model = new Model();
			this.consoleView = new ConsoleView(this);
		}
		public void CreateSaveTemplate(string srcDir, string destDir, string name, int type) {
			this.model.CreateSaveTemplate(srcDir, destDir, name, type);
		}
		public void DeleteSaveTemplate(int templateIndex) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void ExecuteSave(int templateIndex) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void ExecuteAllSave() {
			throw new System.NotImplementedException("Not implemented");
		}
		public string GetAllTemplates() {
			throw new System.NotImplementedException("Not implemented");
		}
	}
}
