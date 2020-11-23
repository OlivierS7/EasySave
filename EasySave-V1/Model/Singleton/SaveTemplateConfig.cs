using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace NSModel.Singleton {
	public class SaveTemplateConfig {
		private static SaveTemplateConfig saveTemplateConfig;
		private FileInfo file;

		private SaveTemplateConfig() {
			string path = "..\\..\\..\\SaveTemplatesConfig.json";
			if (File.Exists(path))
			{
				file = new FileInfo(path);
			}
			else
			{
				using (File.Create(path))
				{
					file = new FileInfo(path);
					File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
				}
			}
		}
		public static SaveTemplateConfig GetSaveTemplateInstance() {
			if (saveTemplateConfig == null)
			{
				saveTemplateConfig = new SaveTemplateConfig();
			}
			return saveTemplateConfig;
		}
		public void Write(SaveTemplate template) {
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);
			string reader = File.ReadAllText(file.ToString());
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates == null)
			{
				templates = new List<SaveTemplate>();
			}
			templates.Add(template);
			StreamWriter writer = new StreamWriter(file.ToString());
			writer.WriteLine(JsonConvert.SerializeObject(templates, Formatting.Indented));
			writer.Close();
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
		}
		public void Delete(SaveTemplate template) {
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);
			string reader = File.ReadAllText(file.ToString());
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates != null)
			{
				templates.RemoveAll(item => item.backupName == template.backupName && item.destDirectory == template.destDirectory);
			}
			StreamWriter writer = new StreamWriter(file.ToString());
			writer.WriteLine(JsonConvert.SerializeObject(templates, Formatting.Indented));
			writer.Close();
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
		}
		public List<SaveTemplate> GetTemplates()
        {
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);
			string reader = File.ReadAllText(file.ToString());
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates == null)
			{
				templates = new List<SaveTemplate>();
			}
			return templates;
		}
	}
}
