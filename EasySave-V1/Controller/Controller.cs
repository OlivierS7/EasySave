using NSModel;
using NSView;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;



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
			this.consoleView.CurrentMenu.ShowMenu(this.consoleView);
		}
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type) {
			string error = "";
			Regex nameForm = new Regex("^[^/\":*?\\<>|]+$");
			Regex directoryName = new Regex(@"^([A-Za-z]:\\|\\)([^/:*?""\<>|]*\\)*[^/:*?""\<>|]*$");
			Match nameMatch = nameForm.Match(name);
			Match srcDirNameMatch = directoryName.Match(srcDir);
			Match destDirNameMatch = directoryName.Match(destDir);
			if(nameMatch.Success && srcDirNameMatch.Success && destDirNameMatch.Success)
            {
				this.model.CreateSaveTemplate(name, srcDir, destDir, type);
			}
            else
			{
				if(!nameMatch.Success)
                {
					error = "Invalid name\n";
                }
				if(!srcDirNameMatch.Success)
                {
					error += "Invalid source directory path format\n";
                }
				if(!destDirNameMatch.Success)
                {
					error += "Invalid destination directory path format\n";
				}
				PrintMessage(error);
            }
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
		public void PrintMessage(string message)
        {
			this.consoleView.PrintMessage(message);
        }
	}
}
