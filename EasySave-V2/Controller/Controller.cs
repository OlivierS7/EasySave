using NSModel;
using NSView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NSController {
	public class Controller {

		private IView _View;
		private Model _model;

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
			//this.consoleView = new ConsoleView(this);
			//this.consoleView.CurrentMenu.ShowMenu(this.consoleView);
			Application.Run(new Form1(this));
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
                try
                {
					this.model.CreateSaveTemplate(name, srcDir, destDir, type);
				}
				catch (Exception err)
                {
					PrintMessage(err.Message);
                }
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
					error += "Invalid destination directory path format";
				}
				PrintMessage(error);
            }
		}
		public void DeleteSaveTemplate(int templateIndex) {
			try
			{
				this.model.DeleteSaveTemplate(templateIndex);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message);
			}
		}
		public void ModifySaveTemplate(int templateIndex, string destDir, int type)
        {
			this.model.ModifySaveTemplate(templateIndex, destDir, type);
        }
		public void ExecuteOneSave(int templateIndex) {
			try
			{
				this.model.ExecuteOneSave(templateIndex);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message);
			}
		}
		public void ExecuteAllSave() {
			try
			{
				this.model.ExecuteAllSave();
			}
			catch (Exception err)
			{
				PrintMessage(err.Message);
			}
		}
		public List<string> GetAllTemplates() {
			List<SaveTemplate> templates = this.model.templates;
			List<string> templatesNames = new List<string>();
			if (this.model.templates.Count == 0)
            {
				throw new Exception("There is no save templates");
            } 
			else
            {
				foreach (SaveTemplate template in templates)
				{
					templatesNames.Add(template.backupName);
				}
			}
			return templatesNames;
		}
		public void OpenLogs()
        {
			this.model.OpenLogs();
		}
		public void PrintMessage(string message)
        {
			//this.consoleView.PrintMessage(message);
        }
		public void Progression(string progression)
        {
			//this.consoleView.Progression(progression);
        }
		public bool maxTemplatesReached()
        {
			if (model.templates.Count == 5)
				return true;
			return false;
        }

		public void Exit()
        {
			Environment.Exit(1);
		}
	}
}
