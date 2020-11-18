using System;
using System.IO;

namespace Save
{
    public class Backup
    {

        private string _backupName;
        private string _srcDirectory;
        private string _destDirectory;
        private DirectoryInfo _source;
        private DirectoryInfo _dest;
        private int _backupType;

        public string backupName
        {
            get => this._backupName;
            set => this._backupName = value;
        }


        public string srcDirectory
        {
            get => this._srcDirectory;
            set { 
                this._srcDirectory = value;
                this._source = new DirectoryInfo(value);
            }
        }

        public string destDirectory
        {
            get => this._destDirectory;
            set {
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

        /* Method to choose which action will be executed */
        public void prepareCopy()
        {
            if(this.backupType == 1)
            {
                NewSaveDirectory();
                CopyAll(this.srcDirectoryInfo, this.destDirectoryInfo);
            } else if (this.backupType == 2){
                Console.WriteLine("sauvegarde incrémentielle");
            } else
            {
                Console.WriteLine("Mauvaise sauvegarde relancer le programme");
            }
        }

        /* Method to create Destination Directory and a Sub Directory */
        public void NewSaveDirectory()
        {
            String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            String TodaysTime = DateTime.Now.ToString("HH-mm-ss");
            if (!Directory.Exists(this.destDirectory))
            {
                // Create SubDirectory with date and time
                Directory.CreateDirectory(this.destDirectory);
                Console.WriteLine(this.destDirectoryInfo);
                this.destDirectoryInfo.CreateSubdirectory(Todaysdate + "_" + TodaysTime);
                this.destDirectoryInfo = new DirectoryInfo(this.destDirectoryInfo + "\\" + Todaysdate + "_" + TodaysTime);
            } else
            {
                
                this.destDirectoryInfo.CreateSubdirectory(Todaysdate + "_" + TodaysTime);
                this.destDirectoryInfo = new DirectoryInfo(this.destDirectory + "\\" + Todaysdate + "_" + TodaysTime);
            }
        }

        /* Method to create a full backup of a directory */
        public void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}