using System;
using System.IO;

namespace NSModel
{
	public class SaveTemplate {
        private string _backupName;
        private string _srcDirectory;
        private string _destDirectory;
        private int _backupType;
        private DirectoryInfo _source;
        private DirectoryInfo _dest;
        private SaveStrategy _saveStrategy;
        //private Model templates;

        public string backupName
        {
            get => this._backupName;
            set => this._backupName = value;
        }


        public string srcDirectory
        {
            get => this._srcDirectory;
            set
            {
                this._srcDirectory = value;
                this._source = new DirectoryInfo(value);
            }
        }

        public string destDirectory
        {
            get => this._destDirectory;
            set
            {
                this._destDirectory = value;
                this._dest = new DirectoryInfo(value);
            }
        }

        public DirectoryInfo srcDirectoryInfo
        {
            get => this._source;
            set => this._source = value;
        }

        public DirectoryInfo destDirectoryInfo
        {
            get => this._dest;
            set => this._dest = value;
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

        public SaveTemplate(string srcDir, string destDir, string name, int type)
        {
            this.srcDirectory = srcDir;
            this.destDirectory = destDir;
            this.backupName = name;
            this.backupType = type;
            this.saveStrategy = new FullSave();
            this.saveStrategy.Execute(this);
        }

	}

}
