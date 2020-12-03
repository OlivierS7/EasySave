using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NSModel.Singleton
{
    class SaveParameter
    {
        private static SaveParameter saveParameter;
        private FileInfo _file;
		private Parameters parameters;
		public class Parameters
		{
			private List<string> cryptExtensions = new List<string>();
			private List<string> allowedProcesses = new List<string>();

            public List<string> CryptExtensions { get => cryptExtensions; set => cryptExtensions = value; }
            public List<string> AllowedProcesses { get => allowedProcesses; set => allowedProcesses = value; }

            public Parameters(List<string> cryptList, List<string> processesList)
            {
				if(cryptList == null)
                {
					this.CryptExtensions = new List<string>();
                }
                else
                {
					this.CryptExtensions = cryptList;
				}
				if (processesList == null)
				{
					this.AllowedProcesses = new List<string>();
				}
				else
                {
					this.AllowedProcesses = processesList;
				}
            }
		}

		public FileInfo file
		{
			get => _file;
			set => _file = value;
		}

		/* Constructor */
		private SaveParameter()
		{
			string path = "..\\..\\..\\SaveParameters.json";
			if (File.Exists(path))
			{
				file = new FileInfo(path);
				string reader = File.ReadAllText(file.ToString());
				this.parameters = JsonConvert.DeserializeObject<Parameters>(reader);
			}
			else
			{
				using (File.Create(path))
				{
					file = new FileInfo(path);
					File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
				}
				this.parameters = new Parameters(null, null);
			}
		}

		/* Method to get Instance of this Singleton*/
		public static SaveParameter GetInstance()
		{
			if (saveParameter == null)
			{
				saveParameter = new SaveParameter();
			}
			return saveParameter;
		}
		public void Write(string parameter, int type)
		{
			if (type != 1 && type != 2)
				throw new Exception("Can only choose type 1 (allowed processes) or type 2 (extensions to crypt)");
			/* Unhiding file to allow edit */
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);

			string reader = File.ReadAllText(file.ToString());

			/* Reading the config file and converting it to a list of SaveTemplates */
			this.parameters = JsonConvert.DeserializeObject<Parameters>(reader);
			if(type == 1)
				this.parameters.AllowedProcesses.Add(parameter);
			if(type == 2)
				this.parameters.CryptExtensions.Add(parameter);
			StreamWriter writer = new StreamWriter(file.ToString());

			/* Converting the list of SaveTemplates to a Json and writing it to config file */
			writer.WriteLine(JsonConvert.SerializeObject(this.parameters, Formatting.Indented));
			writer.Close();

			/* Hiding file */
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
		}

		/* Method to delete a SaveTemplate in the config file */
		public void Delete(int index, int type)
		{
			if (type != 1 && type != 2)
				throw new Exception("Can only choose type 1 (allowed processes) or type 2 (extensions to crypt)");
			/* Unhiding file to allow edit */
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);

			string reader = File.ReadAllText(file.ToString());

			/* Reading the config file and converting it to a list of SaveTemplates */
			this.parameters = JsonConvert.DeserializeObject<Parameters>(reader);
			if (type == 1)
				this.parameters.AllowedProcesses.RemoveAt(index - 1);
			if (type == 2)
				this.parameters.CryptExtensions.RemoveAt(index - 1);
			StreamWriter writer = new StreamWriter(file.ToString());

			/* Converting the list of SaveTemplates to a Json and writing it to config file */
			writer.WriteLine(JsonConvert.SerializeObject(this.parameters, Formatting.Indented));
			writer.Close();

			/* Hiding file */
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
		}
	}
}
