using System;
using System.Collections.Generic;
using System.Threading;

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
			this.templates.Add(new SaveTemplate(name, srcDir, destDir, type));
		}
		public void DeleteSaveTemplate(int templateIndex) {
			this.templates.RemoveAt(templateIndex - 1);
		}
		public void ExecuteOneSave(int templateIndex) {
			try
            {
				SaveTemplate template = IntToSaveTemplate(templateIndex);
				template.saveStrategy.Execute(template);
			}
			catch
            {
				//NE PAS FAIRE L'AFFICHAGE ICI A CHANGER
				Console.WriteLine("Aucune sauvegarde ne correspond");
				Thread.Sleep(1000);
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
	}
}
