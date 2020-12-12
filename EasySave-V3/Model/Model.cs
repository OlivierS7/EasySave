using System;
using System.Diagnostics;
using System.Collections.Generic;
using NSModel.Singleton;
using System.IO;
using EasySave_V3.Properties;
using System.Threading;

namespace NSModel
{
	public class Model
	{
		private static Barrier barrier;
		private static Mutex mutex = new Mutex();
		private static Mutex priorityRunning = new Mutex();
		private static bool priority = false;
		private static int runningPrioritySaves = 0;
		private delegate void deleg();
		private List<SaveTemplate> _templates;

		public List<SaveTemplate> templates
		{
			get => this._templates;
			set => this._templates = value;
		}
        public static Barrier Barrier { get => barrier; set => barrier = value; }
        public static Mutex Mutex { get => mutex; set => mutex = value; }
        public static Mutex PriorityRunning { get => priorityRunning; set => priorityRunning = value; }

        /* Constructor */
        public Model()
		{
			this.templates = SaveTemplateConfig.GetInstance().GetTemplates();
		}
		public static void SetPriority(bool priority)
        {
			priorityRunning.WaitOne();
			Model.priority = priority;
			priorityRunning.ReleaseMutex();
        }
		public static bool GetPriority()
        {
			return Model.priority;
		}
		public static void IncreasePrioritySaves()
        {
			priorityRunning.WaitOne();
			runningPrioritySaves++;
			priorityRunning.ReleaseMutex();
		}
		public static void DecreasePrioritySaves()
		{
			priorityRunning.WaitOne();
			runningPrioritySaves--;
			priorityRunning.ReleaseMutex();
		}
		public static int GetPrioritySaves()
		{
			return Model.runningPrioritySaves;
		}

		/* Method to create a save template */
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type)
		{
			if (type != 1 && type != 2)
				throw new Exception(type + Resources.InvalidType);
			if (srcDir == destDir)
				throw new Exception(Resources.SrcDiffDest);
			if (!Directory.Exists(srcDir))
				throw new Exception(Resources.SrcInexist);
			SaveTemplate template = new SaveTemplate(name, srcDir, destDir, type);
			this.templates.Add(template);
			SaveTemplateConfig.GetInstance().Write(template);
			State.GetInstance().Create(template);
		}

		/* Method to delete a save template */
		public void DeleteSaveTemplate(int templateIndex)
		{
			if (this.templates.Count < templateIndex)
				throw new Exception(templateIndex + ": No save template at this index");
			SaveTemplateConfig.GetInstance().Delete(IntToSaveTemplate(templateIndex));
			State.GetInstance().Delete(IntToSaveTemplate(templateIndex));
			this.templates.RemoveAt(templateIndex - 1);
		}

		/* Method to modify a save template */
		public void ModifySaveTemplate(int templateIndex, string name, string srcDir, string destDir, int type)
		{
			SaveTemplate template = this.IntToSaveTemplate(templateIndex);
			if (type != 1 && type != 2)
				throw new Exception(type + Resources.InvalidType);
			if (template.srcDirectory == destDir)
				throw new Exception(Resources.SrcDiffDest);
			if (!Directory.Exists(srcDir))
				throw new Exception(Resources.SrcInexist);
			SaveTemplateConfig.GetInstance().Delete(template);
			State.GetInstance().Delete(template);
			template.backupName = name;
			template.srcDirectory = srcDir;
			template.destDirectory = destDir;
			template.backupType = type;
			this.templates.RemoveAt(templateIndex - 1);
			this.templates.Add(template);
			SaveTemplateConfig.GetInstance().Write(template);
			State.GetInstance().Create(template);
		}

		/* Method to execute one backup */
		public void ExecuteOneSave(int templateIndex, List<string> extensionsToEncrypt)
		{
			Barrier = new Barrier(participantCount: 1);
			SaveTemplate template = IntToSaveTemplate(templateIndex);
			deleg delg = () =>
			{
				template.saveStrategy.Execute(template, extensionsToEncrypt);
			};
			if (this.templates.Count < templateIndex)
				throw new Exception(templateIndex + ": No save template at this index");
			if (!CheckProcesses())
            {
				Thread save = new Thread(new ThreadStart(delg));
				save.Start();
            }
			else
				throw new Exception(Resources.RunningError);
		}


		/* Method to execute all backups */
		public void ExecuteAllSave(List<string> extensionsToEncrypt)
		{
			if (templates.Count == 0)
				throw new Exception(Resources.NoSaveToExec);
			Barrier = new Barrier(participantCount: templates.Count);
			foreach (SaveTemplate template in templates)
			{
				deleg delg = () =>
				{
					template.saveStrategy.Execute(template, extensionsToEncrypt);
				};
				if (!CheckProcesses())
				{
					Thread save = new Thread(new ThreadStart(delg));
					save.Start();
				}
				else
					throw new Exception(Resources.RunningError);
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
			foreach(string strProcess in getForbiddenProcesses())
            {
				if (Process.GetProcessesByName(strProcess).Length > 0)
					return true;
			}
			return false;
		}

		/* Method to get the forbidden processes from the parameters */
		public List<string> getForbiddenProcesses()
        {
			return SaveParameter.GetInstance().Parameters1.getForbiddenProcesses();
        }
		/* Method to get the extensions to encrypt from the parameters */
		public List<string> getExtensionsToEncrypt()
		{
			return SaveParameter.GetInstance().Parameters1.getCryptExtensions();
		}
		/* Method to get the priority files extensions from the parameters */
		public List<string> getPriorityFilesExtensions()
		{
			return SaveParameter.GetInstance().Parameters1.getPriorityFilesExtensions();
		}

		/* Method to add the forbidden processes to the parameters */
		public void addForbiddenProcess(string process)
        {
			SaveParameter.GetInstance().Write(process, 1);
        }
		/* Method to add the extensions to encrypt to the parameters */
		public void addExtensionToEncrypt(string extension)
		{
			SaveParameter.GetInstance().Write(extension, 2);
		}
		/* Method to add the priority files extensions to the parameters */
		public void addPriorityFilesExtension(string extension)
		{
			SaveParameter.GetInstance().Write(extension, 3);
		}

		/* Method to remove the forbidden processes to the parameters */
		public void removeForbiddenProcess(int index)
        {
			SaveParameter.GetInstance().Delete(index, 1);
        }
		/* Method to remove the extensions to encrypt to the parameters */
		public void removeExtensionToEncrypt(int index)
		{
			SaveParameter.GetInstance().Delete(index, 2);
		}
		/* Method to remove the priority files extensions to the parameters */
		public void removePriorityFilesExtension(int index)
		{
			SaveParameter.GetInstance().Delete(index, 3);
		}
	}
}