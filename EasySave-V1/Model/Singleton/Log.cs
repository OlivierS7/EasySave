using Newtonsoft.Json;
using System;
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
		private string _directoryPath = @"..\..\..\..\Logs";
		private string _fileinfoPath = @"..\..\..\..\Logs\logs.json";


		public FileInfo file
		{
			get => _file;
		}
		public string fileName
		{
			get => _fileName;
			set => _fileName = value;
		}

		private Log()
		{
			dir = new DirectoryInfo(_directoryPath);
			_file = new FileInfo(_fileinfoPath);//à changer
			fileName = _fileinfoPath;
		}
		public static Log GetLogInstance()
		{
			if (log == null)
			{
				log = new Log();
			}
			return log;
		}
		public void Write(SaveTemplate template, long totalSize, TimeSpan time)
		{
			var obj = new 
			{
				BackupName = template.backupName, 
				SourceDirectory = template.srcDirectory, 
				DestinationDirectory = template.destDirectory, 
				Size = totalSize + "Bytes", 
				Time = time  
			};

			if (!File.Exists(_fileinfoPath))
			{
				try
				{
					StreamWriter sw = file.CreateText();
					string output = JsonConvert.SerializeObject(obj,Formatting.Indented);
					sw.Write(output);
					sw.Close();
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception: " + e.Message);
				}
			}
			else
			{
				try
				{
					StreamWriter sw = file.AppendText();
					string output = JsonConvert.SerializeObject(obj,Formatting.Indented);
					sw.Write(output);
					sw.Close();
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception: " + e.Message);
				}
			}
		}
	}
}