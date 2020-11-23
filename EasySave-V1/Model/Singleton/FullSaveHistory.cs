using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NSModel.Singleton {
	public class FullSaveHistory {
		private static FullSaveHistory fullSaveHistory;
		private FileInfo _file;

        public FileInfo file { 
			get => _file; 
			set => _file = value; 
		}

        private FullSaveHistory() {
			string path = "..\\..\\..\\FullSaveHistory.json";
			if (File.Exists(path))
            {
				file = new FileInfo(path);
			} else
            {
				using (File.Create(path))
				{					
					file = new FileInfo(path);
					File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
				}				
			}
		}
		public static FullSaveHistory GetInstance() {
			if (fullSaveHistory == null)
			{
				fullSaveHistory = new FullSaveHistory();
			}
			return fullSaveHistory;
		}
		public void Write(SaveTemplate template, string dateTime) {
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);
            string reader = File.ReadAllText(file.ToString());
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates != null)
            {
				templates.RemoveAll(item => item.srcDirectory == template.srcDirectory);
			} else
            {
				templates = new List<SaveTemplate>();
			}
			template.destDirectory += "\\" + dateTime;
			templates.Add(template);
			StreamWriter writer = new StreamWriter(file.ToString());
			writer.WriteLine(JsonConvert.SerializeObject(templates, Formatting.Indented));
			writer.Close();
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
		}
		public SaveTemplate GetFullSaveForDir(SaveTemplate template) {
			string reader = File.ReadAllText(file.ToString());
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates != null)
			{
				foreach (SaveTemplate item in templates)
                {
					if (item.srcDirectory == template.srcDirectory)
						return item;
                }
			}
			return null;
		}
	}
}
