using System;
using System.Diagnostics;
using System.Collections.Generic;
using NSModel.Singleton;
using System.IO;

namespace NSModel
{
	public class Model
	{

		private List<SaveTemplate> _templates;

		public List<SaveTemplate> templates
		{
			get => this._templates;
			set => this._templates = value;
		}

		/* Constructor */
		public Model()
		{
			this.templates = SaveTemplateConfig.GetInstance().GetTemplates();
		}

		/* Method to create a save template */
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type)
		{
			if (type != 1 && type != 2)
			{
				throw new Exception("  " + type + " isn't a valid type");
			}
			if (srcDir == destDir)
			{
				throw new Exception("  The source directory cannot be the same as the destination directory");
			}
			if (!Directory.Exists(srcDir))
			{
				throw new Exception("  The source directory doesn't exist");
			}
			SaveTemplate template = new SaveTemplate(name, srcDir, destDir, type);
			this.templates.Add(template);
			SaveTemplateConfig.GetInstance().Write(template);
			State.GetInstance().Create(template);
		}

		/* Method to delete a save template */
		public void DeleteSaveTemplate(int templateIndex)
		{
			if (this.templates.Count < templateIndex)
			{
				throw new Exception("  " + templateIndex + ": No save template at this index");
			}
			SaveTemplateConfig.GetInstance().Delete(IntToSaveTemplate(templateIndex));
			State.GetInstance().Delete(IntToSaveTemplate(templateIndex));
			this.templates.RemoveAt(templateIndex - 1);
		}

		public void ModifySaveTemplate(int templateIndex, string destDir, int type)
		{
			SaveTemplate template = this.IntToSaveTemplate(templateIndex);
			if (type != 1 && type != 2)
			{
				throw new Exception("  " + type + " isn't a valid type");
			}
			if (template.srcDirectory == destDir)
			{
				throw new Exception("  The source directory cannot be the same as the destination directory");
			}
			SaveTemplateConfig.GetInstance().Delete(template);
			State.GetInstance().Delete(template);
			template.backupType = type;
			template.destDirectory = destDir;
			this.templates.RemoveAt(templateIndex - 1);
			this.templates.Add(template);
			SaveTemplateConfig.GetInstance().Write(template);
			State.GetInstance().Create(template);
		}

		/* Method to execute one backup */
		public void ExecuteOneSave(int templateIndex)
		{
			if (this.templates.Count < templateIndex)
			{
				throw new Exception("  " + templateIndex + ": No save template at this index");
			}
			SaveTemplate template = IntToSaveTemplate(templateIndex);
			if (!CheckProcesses())
			{
				template.saveStrategy.Execute(template);
			}
			else
			{
				throw new Exception("  A running program blocks the execution of the backup job. Please close it and retry");
			}
		}


		/* Method to execute all backups */
		public void ExecuteAllSave()
		{
			if (templates.Count == 0)
			{
				throw new Exception("  There is no save templates to execute");
			}
			foreach (SaveTemplate template in templates)
			{
				if (!CheckProcesses())
				{
					template.saveStrategy.Execute(template);
				}
				else
				{
					throw new Exception("  A running program blocks the execution of the backup job. Please close it and retry");
				}
			}
		}


		/* Method to get the saveTemplate from the user's input */
		private SaveTemplate IntToSaveTemplate(int templateIndex)
		{
			return this.templates[templateIndex - 1];
		}


		/* Method to open logs */
		public void OpenLogs()
		{
			Process.Start("Notepad.exe", Log.GetInstance().file.ToString());
		}
		public bool CheckProcesses()
		{
			Process[] processes = Process.GetProcesses();
			foreach (Process process in processes)
			{
				if (process.ProcessName.Contains("Calculator"))
				{
					return true;
				}
			}
			return false;
		}
		public List<string> getAllowedProcesses()
        {
			return SaveParameter.GetInstance().Parameters1.getAllowedProcesses();
        }
		public List<string> getExtensionsToEncrypt()
		{
			return SaveParameter.GetInstance().Parameters1.getCryptExtensions();
		}
		public void addAllowedProcess(string process)
        {
			SaveParameter.GetInstance().Write(process, 1);
        }
		public void addExtensionToEncrypt(string extension)
		{
			SaveParameter.GetInstance().Write(extension, 2);
		}
		public void removeAllowedProcess(int index)
        {
			SaveParameter.GetInstance().Delete(index, 1);
        }
		public void removeExtensionToEncrypt(int index)
		{
			SaveParameter.GetInstance().Delete(index, 2);
		}
	}
}