using NSModel;
using NSView;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EasySave_V3.Properties;

namespace NSController {
	public class Controller {

		private IView _View;
		private Model _model;

		/* Variables for Regex */
		private string error = "";
		private Regex nameForm = new Regex("^[^/\":*?\\<>|]+$");
		private Regex directoryName = new Regex(@"^([A-Za-z]:\\|\\)([^/:*?""\<>|]*\\)*[^/:*?""\<>|]*$");

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

		/* Constructor */
		public Controller() {
			this.model = new Model();
			this.View = new GraphicalView(this);
			this.View.Start(); ;
		}
		/* Method to create a save template */
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type) {
			Match nameMatch = nameForm.Match(name);
			Match srcDirNameMatch = directoryName.Match(srcDir);
			Match destDirNameMatch = directoryName.Match(destDir);
			bool isEqual = false;

			/* Checking if informations matches regex */
			if (nameMatch.Success && srcDirNameMatch.Success && destDirNameMatch.Success)
            {
				List<SaveTemplate> templates = this.model.templates;
				foreach(SaveTemplate template in templates)
                {
					if (template.backupName == name)
                    {
						error = Resources.InvalidSameName;
						PrintMessage(error, -1);
						isEqual = true;
					}
				}
                if (!isEqual)
                {
					try
					{
						this.model.CreateSaveTemplate(name, srcDir, destDir, type);
						PrintMessage(Resources.Success, 1);
					}
					catch (Exception err)
					{
						PrintMessage(err.Message, -1);
					}
				}
			}
            else
			{
				if(!nameMatch.Success)
					error = Resources.InvalidName + "\n";
				if(!srcDirNameMatch.Success)
					error += Resources.InvalidSrc + "\n";
				if(!destDirNameMatch.Success)
					error += Resources.InvalidDest;
				PrintMessage(error, -1);
            }
		}

		/* Method to delete a save template */
		public void DeleteSaveTemplate(int templateIndex) {
			try
			{
				this.model.DeleteSaveTemplate(templateIndex);
				PrintMessage(Resources.SuccessDel, 1);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message, -1);
			}
		}

		/* Method to modify an existing save template */
		public void ModifySaveTemplate(int templateIndex, string name, string srcDir, string destDir, int type)
        {
			Match nameMatch = nameForm.Match(name);
			Match srcDirNameMatch = directoryName.Match(srcDir);
			Match destDirNameMatch = directoryName.Match(destDir);

			/* Checking if informations matches regex */
			if (nameMatch.Success && srcDirNameMatch.Success && destDirNameMatch.Success)
			{
				try
				{
					this.model.ModifySaveTemplate(templateIndex, name, srcDir, destDir, type);
					PrintMessage(Resources.SuccessModif, 1);
				}
				catch (Exception err)
				{
					PrintMessage(err.Message, -1);
				}
			} else
            {
				if (!nameMatch.Success)
					error = Resources.InvalidName + "\n";
				if (!srcDirNameMatch.Success)
					error += Resources.InvalidSrc + "\n";
				if (!destDirNameMatch.Success)
					error += Resources.InvalidDest;
				PrintMessage(error, -1);
			}
			
        }

		/* Method to execute one save */
		public void ExecuteOneSave(int templateIndex) {
			try
			{
				List<string> extensionsToEncrypt = getExtensionsToEncrypt();
				this.model.ExecuteOneSave(templateIndex, extensionsToEncrypt);
				PrintMessage(Resources.SuccessExec, 1);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message, -1);
			}
		}

		/* Method to execute all saves */
		public void ExecuteAllSave() {
			try
			{
				List<string> extensionsToEncrypt = getExtensionsToEncrypt();
				this.model.ExecuteAllSave(extensionsToEncrypt);
				PrintMessage(Resources.SuccessExecAll, 1);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message, -1);
			}
		}

		/* Method to get all existing templates */
		public List<string> GetAllTemplates() {
			List<SaveTemplate> templates = this.model.templates;
			List<string> templatesNames = new List<string>();
			if (this.model.templates.Count == 0)
            {
				PrintMessage(Resources.NoSave, -1);
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

		public void StopThread(int index)
        {
			model.StopThread(index);
        }

		public string PauseOrResume(int index, bool play)
        {
			return model.PauseOrResume(index, play);
        }

		/* Method to open logs */
		public void OpenLogs()
        {
			this.model.OpenLogs();
		}

		/* Method to print a popup message */
		public void PrintMessage(string message, int type)
        {
			this.View.PrintMessage(message, type);
        }

		/* Method to exit the program */
		public void Exit()
        {
			Environment.Exit(1);
		}

		/* Method to get all forbidden processes */
		public List<string> getForbiddenProcesses()
		{
			return this.model.getForbiddenProcesses();
		}

		/* Method to get all extensions to encrypt */
		public List<string> getExtensionsToEncrypt()
		{
			return this.model.getExtensionsToEncrypt();
		}
		/* Method to get all priority files extensions */
		public List<string> getpriorityFilesExtensions()
		{
			return this.model.getPriorityFilesExtensions();
		}

		/* Method to get all priority files extensions */
		public int getMaxFileSize()
		{
			return Model.getMaxFileSize();
		}

		/* Method to add a forbidden process */
		public void addForbiddenProcess(string process)
		{
			this.model.addForbiddenProcess(process);
		}

		/* Method to add an extension to encrypt */
		public void addExtensionToEncrypt(string extension)
		{
			this.model.addExtensionToEncrypt(extension);
		}

		/* Method to add a priority file extension */
		public void addPriorityFilesExtension(string extension)
		{
			this.model.addPriorityFilesExtension(extension);
		}
		/* Method to add the max file size */
		public void addMaxFileSize(string size)
		{
			this.model.addMaxFileSize(size);
		}

		/* Method to remove a forbidden process */
		public void removeForbiddenProcess(int index)
        {
			this.model.removeForbiddenProcess(index);
        }

		/* Method to remove an extension to encrypt */
		public void removeExtensionToEncrypt(int index)
		{
			this.model.removeExtensionToEncrypt(index);
		}

		/* Method to remove a priority file extension */
		public void removePriorityFilesExtension(int index)
		{
			this.model.removePriorityFilesExtension(index);
		}
	}
}
