using System;
using System.Diagnostics;
using System.Collections.Generic;
using NSModel.Singleton;

namespace NSModel {
	public class Model {

		private List<SaveTemplate> _templates;

		public List<SaveTemplate> templates
		{
			get => this._templates;
			set => this._templates = value;
		}

		/* Constructor */
		public Model()
        {
			this.templates = new List<SaveTemplate>();
        }

		/* Method to create a save template */
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type) {
			SaveTemplate template = new SaveTemplate(name, srcDir, destDir, type);
			FullSaveHistory.GetInstance();
			FullSaveHistory.GetInstance().Write(template);
			this.templates.Add(template);
		}

		/* Method to delete a save template */
		public void DeleteSaveTemplate(int templateIndex) {
			try
            {
				this.templates.RemoveAt(templateIndex - 1);
			}
			catch
            {
				throw new ArgumentException("This save template doesn't exist. Please Try Again !");
			}
		}

		/* Method to execute one backup */
		public void ExecuteOneSave(int templateIndex) {
			try
            {
				SaveTemplate template = IntToSaveTemplate(templateIndex);
				template.saveStrategy.Execute(template);
			}
			catch
            {
				throw new ArgumentException("This save template doesn't exist. Please Try Again !");
            }			
		}


		/* Method to execute all backups */
		public void ExecuteAllSave()
		{
			foreach (SaveTemplate template in templates)
			{
				template.saveStrategy.Execute(template);
			}
		}


		/* Method to get the saveTemplate from the user's input */
		private SaveTemplate IntToSaveTemplate(int templateIndex)
        {
			return this.templates[templateIndex];
        }


		/* Method to open logs */
		public void OpenLogs()
        {
			Process.Start("Notepad.exe", Log.GetLogInstance().fileName);
		}
	}
}
