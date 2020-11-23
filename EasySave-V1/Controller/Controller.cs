using NSModel;
using NSView;
using System.Collections.Generic;

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
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type) {
			this.model.CreateSaveTemplate(name, srcDir, destDir, type);
		}
		public void DeleteSaveTemplate(int templateIndex) {
			this.model.DeleteSaveTemplate(templateIndex);
		}
		public void ExecuteSave(int templateIndex) {
			this.model.ExecuteOneSave(templateIndex - 1);
		}
		public void ExecuteAllSave() {
			this.model.ExecuteAllSave();
		}
		public List<string> GetAllTemplates() {
			List<SaveTemplate> templates = this.model.templates;
			List<string> templatesNames = new List<string>();
			foreach (SaveTemplate template in templates)
			{
				templatesNames.Add(template.backupName);
			}
			return templatesNames;
		}
		public void OpenLogs()
        {
			this.model.OpenLogs();
		}
	}
}
