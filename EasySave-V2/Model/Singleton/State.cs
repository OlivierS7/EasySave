using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NSModel.Singleton {
	public class State {
		private static State state;
		private FileInfo file;
		public List<SaveTemplateState> templatesState = new List<SaveTemplateState>();

		public class SaveTemplateState
		{
			public DateTime date;
			public SaveTemplate template;
			public string save;
			public bool isActive;
			public string srcFile;
			public string destFile;
			public long fileSize;
			public long totalSize;
			public long sizeLeft;
			public int totalFiles;
			public int filesLeft;
			public TimeSpan time;
			public float progression;

			public SaveTemplateState(DateTime date, SaveTemplate template, string saveType, bool isActive, string srcFile, string destFile, long fileSize, long totalSize, long sizeLeft, int totalFiles, int filesLeft, TimeSpan time)
			{
				this.date = date;
				this.template = template;
				this.save = saveType;
				this.isActive = isActive;
				this.srcFile = srcFile;
				this.destFile = destFile;
				this.totalSize = totalSize;
				this.fileSize = fileSize;
				this.sizeLeft = sizeLeft;
				this.totalFiles = totalFiles;
				this.filesLeft = filesLeft;
				this.time = time;
				if(totalSize == 0)
                {
					this.progression = 0;
                }
                else
                {
					this.progression = 100 - ((sizeLeft * 100) / totalSize);
				}
			}
		}

		private State() {
			string path = "..\\..\\..\\State.json";
			if (File.Exists(path))
			{
				file = new FileInfo(path);
				string reader = File.ReadAllText(file.ToString());
				templatesState = JsonConvert.DeserializeObject<List<SaveTemplateState>>(reader);
			}
			else
			{
				using (File.Create(path))
				{
					file = new FileInfo(path);
				}
			}
		}
		public static State GetInstance() {
			if (state == null)
			{
				state = new State();
			}
			return state;
		}
		public void Write(DateTime date, SaveTemplate template, bool isActive, string srcFile, string destFile, long fileSize, long totalSize, long sizeLeft, int totalFiles, int filesLeft, TimeSpan time)
		{
			string reader = File.ReadAllText(file.ToString());
			templatesState = JsonConvert.DeserializeObject<List<SaveTemplateState>>(reader);
			if (this.templatesState == null)
            {
				this.templatesState = new List<SaveTemplateState>();
            }
			string saveType;
			if(template.backupType == 1)
            {
				saveType = "FullSave";
            }
            else
            {
				saveType = "DifferentialSave";
            }
			SaveTemplateState currentTemplate = new SaveTemplateState(date, template, saveType, isActive, srcFile, destFile, fileSize, totalSize, sizeLeft, totalFiles, filesLeft, time);
			int count = 0;
			int index = 0;
			bool replaced = false;
			foreach(SaveTemplateState getTemplate in templatesState)
			{
				if(getTemplate.template.backupName == currentTemplate.template.backupName && getTemplate.template.srcDirectory == currentTemplate.template.srcDirectory)
                {
					replaced = true;
					index = count;
				}
				count++;
			}
            if (!replaced)
            {
				templatesState.Add(currentTemplate);
            } 
			else
            {
				templatesState.RemoveAt(index);
				templatesState.Insert(index, currentTemplate);
			}
			StreamWriter writer = new StreamWriter(file.ToString());
			string output = JsonConvert.SerializeObject(templatesState, Formatting.Indented);
			writer.Write(output);
			writer.Close();
		}
		public void Create(SaveTemplate template)
		{
			string reader = File.ReadAllText(file.ToString());
			templatesState = JsonConvert.DeserializeObject<List<SaveTemplateState>>(reader);
			if (this.templatesState == null)
			{
				this.templatesState = new List<SaveTemplateState>();
			}
			string saveType;
			if (template.backupType == 1)
			{
				saveType = "FullSave";
			}
			else
			{
				saveType = "DifferentialSave";
			}
			this.templatesState.Add(new SaveTemplateState(DateTime.Now, template, saveType, false, null, null, 0, 0, 0, 0, 0, TimeSpan.Zero));
			StreamWriter writer = new StreamWriter(file.ToString());
			string output = JsonConvert.SerializeObject(templatesState, Formatting.Indented);
			writer.Write(output);
			writer.Close();
		}
		public void Delete(SaveTemplate template)
        {
			string reader = File.ReadAllText(file.ToString());
			templatesState = JsonConvert.DeserializeObject<List<SaveTemplateState>>(reader);
			int index = 0;
			int count = 0;
			foreach(SaveTemplateState getTemplate in templatesState)
			{
				if(getTemplate.template.backupName == template.backupName && getTemplate.template.srcDirectory == template.srcDirectory)
                {
					index = count;
					count++;
				}
			}
			this.templatesState.RemoveAt(index);
			StreamWriter writer = new StreamWriter(file.ToString());
			string output = JsonConvert.SerializeObject(templatesState, Formatting.Indented);
			writer.Write(output);
			writer.Close();
		}
	}
}
