using System;
using NSController;

namespace NSModel {
	public class Model {

		private SaveTemplate _templates;
		//private Save _save;
		private Controller _controller;

		public SaveTemplate templates
		{
			get => this._templates;
			set => this._templates = value;
		}
		public void CreateSaveTemplate(string srcDir, string destDir, string name, int type) {
			templates = new SaveTemplate(srcDir, destDir, name, type);
			ExecuteSave(1);
		}
		public void DeleteSaveTemplate(int templateIndex) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void ExecuteSave(int templateIndex) {
			//this.templates.saveStrategy.Execute();
		}



	}

}
