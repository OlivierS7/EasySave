using System;
using System.IO;

namespace NSModel.Singleton {
	public class SaveTemplateConfig {
		private static SaveTemplateConfig saveTemplateConfig;
		private FileInfo file;

		private SaveTemplateConfig() {
			throw new System.NotImplementedException("Not implemented");
		}
		public static SaveTemplateConfig GetSaveTemplateInstance() {
			throw new System.NotImplementedException("Not implemented");
		}
		private void Write(SaveTemplate template) {
			throw new System.NotImplementedException("Not implemented");
		}
		private void Delete(SaveTemplate template) {
			throw new System.NotImplementedException("Not implemented");
		}
	}
}
