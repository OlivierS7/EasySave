using System;
using System.IO;
using System.Linq;

namespace NSModel.Singleton {
	public class Log {
		private static Log log;
		private DirectoryInfo dir;
		private FileInfo _file;
		private string _fileName;
		private string _directoryPath = @"..\..\..\..\Logs";
		private string _fileinfoPath = @"..\..\..\..\Logs\test.txt";


		public FileInfo file {
			get => _file;
		}
        public string fileName {
			get => _fileName;
			set => _fileName = value;
		}

        private Log() {
			dir = new DirectoryInfo(_directoryPath);
			_file = new FileInfo(_fileinfoPath);//à changer
			fileName = "..\\..\\..\\..\\Logs\\test.txt";
		}
/*		public Int64 DirectoryLength(DirectoryInfo folder)
        {
			Int64 bytes = 0;
            foreach (FileInfo fi in folder.GetFiles())
            {
				bytes += fi.Length;
            }
            foreach (DirectoryInfo i in folder.GetDirectories())
            {
				bytes += DirectoryLength(i);
            }
			return bytes;
        }*/
		public static Log GetLogInstance() {
			if (log == null)
			{
				log = new Log();
			}
			return log;
		}
/*		private void LengthStats(SaveTemplate template)
        {
			Int64 size = DirectoryLength(new DirectoryInfo(template.srcDirectoryInfo));
        }*/
		public void Write(SaveTemplate template, Int64 totalSize) {
            try
            {
				StreamWriter sw = new StreamWriter(_fileinfoPath);
                sw.Write(
				"----" + template.backupName + "----" +
				"{ \"\nSource address: \"" + template.srcDirectoryInfo +
				"\"\nDestination address: \"" + template.destDirectoryInfo + 
				"\nTotal size: " + totalSize + 
				"\nTime: " +
				"\n----" + "fin" +template.backupName + "----\n"
				);
				 

				sw.Close();
			}
            catch (Exception e)
            {

				Console.WriteLine("Exception: " + e.Message);
			}
		}
		private string CheckExistingLogs() {
			throw new System.NotImplementedException("Not implemented");
		}
	}
}
