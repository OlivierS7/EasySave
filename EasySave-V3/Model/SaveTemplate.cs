using EasySave_V3.Properties;
using NSModel.Singleton;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace NSModel
{
    [DataContract]
	public class SaveTemplate {
        /* Parameters for the Serialization */
        [DataMember(Name = "name")]
        private string _backupName;
        [DataMember(Name = "srcDir")]
        private string _srcDirectory;
        [DataMember(Name = "destDir")]
        private string _destDirectory;
        [DataMember(Name = "type")]
        private int _backupType;
        private SaveStrategy _saveStrategy;
        public delegate void TemplateStatusDelegate(string status);
        public TemplateStatusDelegate refreshStatusDelegate;
        public delegate void TemplateProgressDelegate(float progression);
        public event TemplateProgressDelegate refreshProgressDelegate;

        public string backupName
        {
            get => this._backupName;
            set => this._backupName = value;
        }


        public string srcDirectory
        {
            get => this._srcDirectory;
            set => this._srcDirectory = value;
        }

        public string destDirectory
        {
            get => this._destDirectory;
            set => this._destDirectory = value;
        }

        public int backupType
        {
            get => this._backupType;
            set => this._backupType = value;
        }

        public SaveStrategy saveStrategy
        {
            get => this._saveStrategy;
            set => this._saveStrategy = value;
        }

        /* Constructor */
        public SaveTemplate(string name, string srcDir, string destDir, int type)
        {
            /* Couting files in directory and subdirectories */
            string[] allFiles = Directory.GetFiles(srcDir, ".", SearchOption.AllDirectories);
            long totalSize = 0;
            foreach (string currentFile in allFiles)
            {
                FileInfo info = new FileInfo(currentFile);
                totalSize += info.Length;
            }
            this.backupName = name;
            this.srcDirectory = srcDir;
            this.destDirectory = destDir;
            this.backupType = type;
            if (type == 1)
                this.saveStrategy = new FullSave();
            else if (type == 2)
                this.saveStrategy = new DifferentialSave();
            this.saveStrategy.refreshStatusDelegate += (Status) => { refreshStatusDelegate?.Invoke(Status); };
            this.saveStrategy.refreshProgressDelegate += (progression) => { refreshProgressDelegate?.Invoke(progression); };
        }
    }
}
