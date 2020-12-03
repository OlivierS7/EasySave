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
			public List<string> cryptExtensions = new List<string>();
			public List<string> forbiddenProcesses = new List<string>();

			public List<string> getCryptExtensions()
            {
				if (cryptExtensions == null)
                {
					this.cryptExtensions = new List<string>();
                }
				return cryptExtensions;
            }
			public List<string> getForbiddenProcesses()
			{
				if (forbiddenProcesses == null)
				{
					this.forbiddenProcesses = new List<string>();
				}
				return forbiddenProcesses;
			}

			public Parameters(List<string> cryptList, List<string> processesList)
            {
				if(cryptList == null)
                {
					this.cryptExtensions = new List<string>();
                }
                else
                {
					this.cryptExtensions = cryptList;
				}
				if (processesList == null)
				{
					this.forbiddenProcesses = new List<string>();
				}
				else
                {
					this.forbiddenProcesses = processesList;
				}
            }
		}

		public FileInfo file
		{
			get => _file;
			set => _file = value;
		}
        public Parameters Parameters1 { get => parameters; set => parameters = value; }

        /* Constructor */
        private SaveParameter()
		{
			string path = "..\\..\\..\\SaveParameters.json";
			if (File.Exists(path))
			{
				file = new FileInfo(path);
				string reader = File.ReadAllText(file.ToString());
				this.Parameters1 = JsonConvert.DeserializeObject<Parameters>(reader);
				if (this.Parameters1 == null)
					this.Parameters1 = new Parameters(null, null);
			}
			else
			{
				using (File.Create(path))
				{
					file = new FileInfo(path);
					File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
				}
				this.Parameters1 = new Parameters(null, null);
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
				throw new Exception("Can only choose type 1 (forbidden processes) or type 2 (extensions to crypt)");
			/* Unhiding file to allow edit */
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);

			string reader = File.ReadAllText(file.ToString());

			/* Reading the config file and converting it to a list of SaveTemplates */
			this.Parameters1 = JsonConvert.DeserializeObject<Parameters>(reader);
			if (this.Parameters1 == null)
			{
				this.Parameters1 = new Parameters(null, null);
			}
			if (type == 1)
				this.Parameters1.forbiddenProcesses.Add(parameter);
			if(type == 2)
				this.Parameters1.cryptExtensions.Add(parameter);
			StreamWriter writer = new StreamWriter(file.ToString());

			/* Converting the list of SaveTemplates to a Json and writing it to config file */
			writer.WriteLine(JsonConvert.SerializeObject(this.Parameters1, Formatting.Indented));
			writer.Close();

			/* Hiding file */
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
		}

		/* Method to delete a SaveTemplate in the config file */
		public void Delete(int index, int type)
		{
			if (type != 1 && type != 2)
				throw new Exception("Can only choose type 1 (forbidden processes) or type 2 (extensions to crypt)");
			/* Unhiding file to allow edit */
			var attributes = File.GetAttributes(file.ToString());
			attributes &= ~FileAttributes.Hidden;
			File.SetAttributes(file.ToString(), attributes);

			string reader = File.ReadAllText(file.ToString());

			/* Reading the config file and converting it to a list of SaveTemplates */
			this.Parameters1 = JsonConvert.DeserializeObject<Parameters>(reader);
			if (type == 1)
				this.Parameters1.forbiddenProcesses.RemoveAt(index - 1);
			if (type == 2)
				this.Parameters1.cryptExtensions.RemoveAt(index - 1);
			StreamWriter writer = new StreamWriter(file.ToString());

			/* Converting the list of SaveTemplates to a Json and writing it to config file */
			writer.WriteLine(JsonConvert.SerializeObject(this.Parameters1, Formatting.Indented));
			writer.Close();

			/* Hiding file */
			File.SetAttributes(file.ToString(), File.GetAttributes(file.ToString()) | FileAttributes.Hidden);
		}
	}
}
