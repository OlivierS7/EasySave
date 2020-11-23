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
			this.templates = SaveTemplateConfig.GetInstance().GetTemplates();
        }

		/* Method to create a save template */
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type) {
			if(type != 1 && type != 2)
            {
				throw new Exception(type + " isn't a valid type");
            }
			SaveTemplate template = new SaveTemplate(name, srcDir, destDir, type);
			this.templates.Add(template);
			SaveTemplateConfig.GetInstance().Write(template);
		}

		/* Method to delete a save template */
		public void DeleteSaveTemplate(int templateIndex) {
			if (this.templates.Count < templateIndex)
            {
				throw new Exception(templateIndex + ": No save template at this index");
			}
			SaveTemplateConfig.GetInstance().Delete(IntToSaveTemplate(templateIndex));
			this.templates.RemoveAt(templateIndex - 1);
		}

		/* Method to execute one backup */
		public void ExecuteOneSave(int templateIndex) {
			if (this.templates.Count < templateIndex)
			{
				throw new Exception(templateIndex + ": No save template at this index");
			}
			SaveTemplate template = IntToSaveTemplate(templateIndex);
			template.saveStrategy.Execute(template);		
		}


		/* Method to execute all backups */
		public void ExecuteAllSave()
		{
			if (templates.Count == 0)
            {
				throw new Exception("There is no save templates to execute");
			}
			foreach (SaveTemplate template in templates)
			{
				template.saveStrategy.Execute(template);
			}
		}


		/* Method to get the saveTemplate from the user's input */
		private SaveTemplate IntToSaveTemplate(int templateIndex)
        {
			return this.templates[templateIndex - 1];
        }


		/* Method to open logs */
		public void OpenLogs()
        {
			Process.Start("Notepad.exe", Log.GetLogInstance().fileName);
		}
	}
}
