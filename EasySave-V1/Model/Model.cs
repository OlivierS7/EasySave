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

		public Model()
        {
			this.templates = new List<SaveTemplate>();
        }
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type) {
			SaveTemplate template = new SaveTemplate(name, srcDir, destDir, type);
			FullSaveHistory.GetInstance();
			FullSaveHistory.GetInstance().Write(template);
			this.templates.Add(template);
		}
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

		public void ExecuteAllSave()
		{
			foreach (SaveTemplate template in templates)
			{
				template.saveStrategy.Execute(template);
			}
		}

		public SaveTemplate IntToSaveTemplate(int templateIndex)
        {
			return this.templates[templateIndex];
        }

		public void OpenLogs()
        {
			Process.Start("Notepad.exe", Log.GetLogInstance().fileName);
		}
	}
}
