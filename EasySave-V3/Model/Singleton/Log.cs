using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace NSModel.Singleton
{
	public class Log
	{
		private static Mutex mutex = new Mutex();
		private static Log log;
		private FileInfo _file;
		private string _fileName;
		private string _directoryPath = @"..\..\..\Logs";
		public List<LogObject> logObjects = new List<LogObject>();

		/* Nested class */
		public class LogObject
		{
			public string SaveTemplateName;
			public string SourceDirectory;
			public string DestinationDirectory;
			public string Size;
			public TimeSpan Time;
			public string Hour;
			public string CryptDuration;

			public LogObject(string SaveTemplateName, string SourceDirectory, string DestinationDirectory, string Size, TimeSpan Time, string CryptDuration)
			{
				this.SaveTemplateName = SaveTemplateName;
				this.SourceDirectory = SourceDirectory;
				this.DestinationDirectory = DestinationDirectory;
				this.Size = Size;
				this.Time = Time;
				this.Hour = DateTime.Now.ToString("HH:mm:ss");
				this.CryptDuration = CryptDuration;

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
        public static Mutex Mutex { get => mutex; set => mutex = value; }

		/* Constructor */
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
				StreamWriter writer = new StreamWriter(file.ToString());
				writer.Write("[]");
				writer.Close();
			}
		}

		/* Get instance of Log */
		public static Log GetInstance()
		{
			if (log == null)
				log = new Log();
			return log;
		}

		/* Method to write logs */
		public void Write(string name, FileInfo srcFile, FileInfo destFile, long fileSize, TimeSpan time, string cryptDuration)
		{
			/* Waiting for mutex to avoid multiple threads writing at the same time */
			Mutex.WaitOne();
			/* Getting all the logs in a list and adding new logs */
			LogObject currentFile = new LogObject(name, srcFile.ToString(), destFile.ToString(), fileSize.ToString() + " bytes", time, cryptDuration + "ms");
			logObjects.Add(currentFile);
			StreamWriter writer = new StreamWriter(file.ToString());
			string output = JsonConvert.SerializeObject(logObjects, Formatting.Indented);
			writer.Write(output);
			writer.Close();
			Mutex.ReleaseMutex();
		}
	}
}