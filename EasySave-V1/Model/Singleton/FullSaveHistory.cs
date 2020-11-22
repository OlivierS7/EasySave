using Newtonsoft.Json;
using System;
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
					File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
					file = new FileInfo(path);
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
		public void Write(SaveTemplate template) {
			string path = "..\\..\\..\\tempFullSaveHistory.json";
			var obj = new { srcDir = template.srcDirectory, destDir = template.destDirectory };
			string lineToInsert = JsonConvert.SerializeObject(obj);
			string stringToCompare = template.srcDirectory.Replace("\\", "\\\\");
			string currentLine;
			bool exist = false;
			File.Copy(file.ToString(), path);
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);
			StreamReader reader = new StreamReader(path);
			StreamWriter writer = new StreamWriter(file.ToString());
			while ((currentLine = reader.ReadLine()) != null)
			{
				if (currentLine.Contains("\"srcDir\":\"" + stringToCompare + "\""))
				{
					exist = true;
					writer.WriteLine(lineToInsert);
				} 
				else
				{
					writer.WriteLine(currentLine);
				}				
			}
			if (!exist)
            {
				writer.WriteLine(lineToInsert);
            }
			reader.Close();
			writer.Close();
			File.SetAttributes(file.ToString(), File.GetAttributes(path) | FileAttributes.Hidden);
			File.Delete(path);
			//File.Move(path, file.ToString());
		}
		private string GetFullSaveForDir(string srcDir) {
			throw new System.NotImplementedException("Not implemented");
		}
	}
}
