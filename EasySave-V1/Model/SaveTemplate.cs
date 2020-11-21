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

        public SaveTemplate(string name, string srcDir, string destDir, int type)
        {
            this.backupName = name;
            this.srcDirectory = srcDir;
            this.destDirectory = destDir;
            this.backupType = type;
            if (type == 1)
            {
                this.saveStrategy = new FullSave();
            }
            else if (type == 2)
            {
                this.saveStrategy = new DifferentialSave();
            } else
            {
                throw new ArgumentException("Type of Save isn't valid. Please try again !");
            }
        }

	}

}
