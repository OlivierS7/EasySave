using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace NSModel.Singleton {
	public class FullSaveHistory {
		private static Mutex mutex = new Mutex();
		private static FullSaveHistory fullSaveHistory;
		private FileInfo _file;

        public FileInfo file { 
			get => _file; 
			set => _file = value; 
		}
        public static Mutex Mutex { get => mutex; set => mutex = value; }

        /* Constructor */
        private FullSaveHistory() {
			string path = "..\\..\\..\\FullSaveHistory.json";
			if (File.Exists(path))
				file = new FileInfo(path);
			else
            {
				using (File.Create(path))
				{					
					file = new FileInfo(path);
					File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
				}				
			}
		}

		/* Method to get Instance of this Singleton*/
		public static FullSaveHistory GetInstance() {
			if (fullSaveHistory == null)
				fullSaveHistory = new FullSaveHistory();
			return fullSaveHistory;
		}

		/* Method to write into FullSaveHistory.json to save the full backups */
		public void Write(SaveTemplate template, string dateTime) {
			/* Waiting for mutex to avoid multiple threads writing at the same time */
			Mutex.WaitOne();
			/* Unhiding file to allow edit */
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);
            string reader = File.ReadAllText(file.ToString());

			/* Reading the config file and converting it to a list of SaveTemplates */
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates != null)
				templates.RemoveAll(item => item.srcDirectory == template.srcDirectory);
			else
				templates = new List<SaveTemplate>();
			SaveTemplate currentTemplate = new SaveTemplate(template.backupName, template.srcDirectory, template.destDirectory + "\\" + dateTime, template.backupType);
			templates.Add(currentTemplate);

			/* Converting the list of SaveTemplates to a Json and writing it to config file */
			StreamWriter writer = new StreamWriter(file.ToString());
			writer.WriteLine(JsonConvert.SerializeObject(templates, Formatting.Indented));
			writer.Close();

			/* Hiding file */
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
			Mutex.ReleaseMutex();
		}

		/* Method to get the last full save for a source directory */
		public SaveTemplate GetFullSaveForDir(SaveTemplate template) {
			string reader = File.ReadAllText(file.ToString());
			List<SaveTemplate> templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(reader);
			if (templates != null)
			{
				foreach (SaveTemplate item in templates)
                {
					if (item.srcDirectory == template.srcDirectory)
                    {
						if(!new DirectoryInfo(item.srcDirectory).Exists)
							return null;
						return item;
					}
                }
			}
			return null;
		}
	}
}
