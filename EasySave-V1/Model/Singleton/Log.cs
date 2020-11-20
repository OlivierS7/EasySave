using System;
using System.IO;

namespace NSModel.Singleton {
	public class Log {
		private static Log log;
		private DirectoryInfo dir;
		private FileInfo file;

		private Log() {
			throw new System.NotImplementedException("Not implemented");
		}
		public static Log GetLogInstance() {
			throw new System.NotImplementedException("Not implemented");
		}
		private void Write(SaveTemplate template, int totalSize, DateTime time) {
			throw new System.NotImplementedException("Not implemented");
		}
		private string CheckExistingLogs() {
			throw new System.NotImplementedException("Not implemented");
		}
	}
}
