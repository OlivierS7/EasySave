using NSModel;
using NSView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NSController {
	public class Controller {

		private Form1 _View;
		private Model _model;

		public Form1 View
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
			this.View = new Form1(this);
			Application.Run(this.View);
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
					PrintMessage("Successfully created the save template", 1);
				}
				catch (Exception err)
                {
					PrintMessage(err.Message, -1);
                }
			}
            else
			{
				if(!nameMatch.Success)
					error = "Invalid name, please try again";
				if(!srcDirNameMatch.Success)
					error += "Invalid source directory path format";
				if(!destDirNameMatch.Success)
					error += "Invalid destination directory path format";
				PrintMessage(error, -1);
            }
		}
		public void DeleteSaveTemplate(int templateIndex) {
			try
			{
				this.model.DeleteSaveTemplate(templateIndex);
				PrintMessage("Successfully deleted the save template", 1);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message, -1);
			}
		}
		public void ModifySaveTemplate(int templateIndex, string name, string srcDir, string destDir, int type)
        {
			string error = "";
			Regex nameForm = new Regex("^[^/\":*?\\<>|]+$");
			Regex directoryName = new Regex(@"^([A-Za-z]:\\|\\)([^/:*?""\<>|]*\\)*[^/:*?""\<>|]*$");
			Match nameMatch = nameForm.Match(name);
			Match srcDirNameMatch = directoryName.Match(srcDir);
			Match destDirNameMatch = directoryName.Match(destDir);
			if (nameMatch.Success && srcDirNameMatch.Success && destDirNameMatch.Success)
			{
				try
				{
					this.model.ModifySaveTemplate(templateIndex, name, srcDir, destDir, type);
					PrintMessage("Successfully modified the save template", 1);
				}
				catch (Exception err)
				{
					PrintMessage(err.Message, -1);
				}
			} else
            {
				if (!nameMatch.Success)
				{
					error = "Invalid name\n";
				}
				if (!srcDirNameMatch.Success)
				{
					error += "Invalid source directory path format\n";
				}
				if (!destDirNameMatch.Success)
				{
					error += "Invalid destination directory path format";
				}
				PrintMessage(error, -1);
			}
			
        }
		public void ExecuteOneSave(int templateIndex) {
			try
			{
				this.model.ExecuteOneSave(templateIndex);
				PrintMessage("Successfully executed the save", 1);
			}
			catch (Exception err)
			{
				PrintMessage(err.ToString(), -1);
			}
		}
		public void ExecuteAllSave() {
			try
			{
				this.model.ExecuteAllSave();
				PrintMessage("Successfully executed all saves", 1);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message, -1);
			}
		}
		public List<string> GetAllTemplates() {
			List<SaveTemplate> templates = this.model.templates;
			List<string> templatesNames = new List<string>();
			if (this.model.templates.Count == 0)
            {
				PrintMessage("There is no save templates, please create one", -1);
            } 
			else
            {
				foreach (SaveTemplate template in templates)
				{
					templatesNames.Add(template.backupName);
					templatesNames.Add(template.srcDirectory);
					templatesNames.Add(template.destDirectory);
					templatesNames.Add(template.backupType.ToString());
				}
			}
			return templatesNames;
		}
		public void OpenLogs()
        {
			this.model.OpenLogs();
		}
		public void PrintMessage(string message, int type)
        {
			this.View.PrintMessage(message, type);
        }

		public void Exit()
        {
			Environment.Exit(1);
		}
		public List<string> getForbiddenProcesses()
		{
			return this.model.getForbiddenProcesses();
		}
		public List<string> getExtensionsToEncrypt()
		{
			return this.model.getExtensionsToEncrypt();
		}
		public void addForbiddenProcess(string process)
		{
			this.model.addForbiddenProcess(process);
		}
		public void addExtensionToEncrypt(string extension)
		{
			this.model.addExtensionToEncrypt(extension);
		}
		public void removeForbiddenProcess(int index)
        {
			this.model.removeForbiddenProcess(index);
        }
		public void removeExtensionToEncrypt(int index)
		{
			this.model.removeExtensionToEncrypt(index);
		}
	}
}
