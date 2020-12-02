using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NSModel.Singleton {
	public class SaveTemplateConfig {
		private static SaveTemplateConfig saveTemplateConfig;
		private FileInfo file;

		/* SaveTemplateConfig constructor */
		private SaveTemplateConfig() {
			/* Create the config file if it doesn't exist already and hide it */
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

					/* Hiding file */
					File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
				}
			}
		}

		/* Method to get Instance of this Singleton*/
		public static SaveTemplateConfig GetInstance() {
			if (saveTemplateConfig == null)
			{
				saveTemplateConfig = new SaveTemplateConfig();
			}
			return saveTemplateConfig;
		}

		/* Method to add a SaveTemplate in the config file */
		public void Write(SaveTemplate template) {

			if(new DirectoryInfo(template.srcDirectory).Exists)
            {
				/* Unhiding file to allow edit */
				var attributes = File.GetAttributes(file.ToString());
				attributes &= ~FileAttributes.Hidden;
				File.SetAttributes(file.ToString(), attributes);

				string reader = File.ReadAllText(file.ToString());

				/* Reading the config file and converting it to a list of SaveTemplates */
				List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
				if (templates == null)
				{
					templates = new List<SaveTemplate>();
				}
				templates.Add(template);
				StreamWriter writer = new StreamWriter(file.ToString());

				/* Converting the list of SaveTemplates to a Json and writing it to config file */
				writer.WriteLine(JsonConvert.SerializeObject(templates, Formatting.Indented));
				writer.Close();

				/* Hiding file */
				File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
			}
			else
            {
				throw new Exception("The source directory doesn't exist");
            }
		}

		/* Method to delete a SaveTemplate in the config file */
		public void Delete(SaveTemplate template) {

			/* Unhiding file to allow edit */
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);

			string reader = File.ReadAllText(file.ToString());

			/* Reading the config file and converting it to a list of SaveTemplates */
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates != null)
			{
				/* Removing the template from the config file */
				templates.RemoveAll(item => item.backupName == template.backupName && item.destDirectory == template.destDirectory && item.srcDirectory == template.srcDirectory);
			}
			StreamWriter writer = new StreamWriter(file.ToString());

			/* Converting the list of SaveTemplates to a Json and writing it to config file */
			writer.WriteLine(JsonConvert.SerializeObject(templates, Formatting.Indented));
			writer.Close();

			/* Hiding file */
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
		}

		/* Method to get all SaveTemplates in the config file */
		public List<SaveTemplate> GetTemplates()
        {
			/* Unhiding file to allow edit */
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);

			string reader = File.ReadAllText(file.ToString());

			/* Reading the config file and converting it to a list of SaveTemplates */
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates == null)
			{
				templates = new List<SaveTemplate>();
			}

			/* Hiding file */
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
			return templates;
		}
	}
}
