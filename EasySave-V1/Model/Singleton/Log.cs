using System;
using System.IO;

namespace NSModel.Singleton
{
	public class Log
	{
		private static Log log;
		private DirectoryInfo dir;
		private FileInfo _file;
		private string _fileName;

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
			dir = new DirectoryInfo("..\\..\\..\\..\\Logs");
			_file = new FileInfo("..\\..\\..\\..\\Logs\\logsTest.txt");//à changer
			fileName = "..\\..\\..\\..\\Logs\\logsTest.txt";
		}
		public static Log GetLogInstance()
		{
			if (log == null)
			{
				log = new Log();
			}
			return log;
		}
		private void Write(SaveTemplate template, int totalSize, DateTime time)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		private string CheckExistingLogs()
		{
			throw new System.NotImplementedException("Not implemented");
		}
	}
}