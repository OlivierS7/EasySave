using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NSModel.Singleton
{
	public class Log
	{
		private static Log log;
		private DirectoryInfo dir;
		private FileInfo _file;
		private string _fileName;
		private string _directoryPath = @"..\..\..\Logs";
		public List<LogObject> logObjects = new List<LogObject>();

		public class LogObject
        {
			public string SaveTemplateName;
			public string SourceDirectory;
			public string DestinationDirectory;
			public string Size;
			public TimeSpan Time;
			public string Hour;

			public LogObject(string SaveTemplateName, string SourceDirectory ,string DestinationDirectory, string Size, TimeSpan Time)
            {
				this.SaveTemplateName = SaveTemplateName;
				this.SourceDirectory = SourceDirectory;
				this.DestinationDirectory = DestinationDirectory;
				this.Size = Size;
				this.Time = Time;
				this.Hour = DateTime.Now.ToString("HH:mm:ss");
            }
		}

		public FileInfo file
		{
			get => _file;
			set => _file = value;
		}
		public string fileName
		{
			get => _fileName;
			set => _fileName = value;
		}

		private Log()
		{
			String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
			string currentLog = _directoryPath + "\\" + "logs-" + Todaysdate + ".json";
			new FileInfo(currentLog).Directory.Create();
			if (File.Exists(currentLog))
			{
				file = new FileInfo(currentLog);
				string reader = File.ReadAllText(file.ToString());
				/* Reading the config file and converting it to a list of LogObjects */
				logObjects = JsonConvert.DeserializeObject<List<LogObject>>(reader);
			}
			else
			{
				using (File.Create(currentLog))
				{
					file = new FileInfo(currentLog);
				}
			}
		}
		public static Log GetInstance()
		{
			if (log == null)
			{
				log = new Log();
			}
			return log;
		}
		public void Write(string name, FileInfo srcFile, FileInfo destFile, long fileSize, TimeSpan time)
		{
			LogObject currentFile = new LogObject(name, srcFile.ToString(), destFile.ToString(), fileSize.ToString()+" bytes", time);
			logObjects.Add(currentFile);
			StreamWriter writer = new StreamWriter(file.ToString());
			string output = JsonConvert.SerializeObject(logObjects,Formatting.Indented);
			writer.Write(output);
			writer.Close();
		}
	}
}