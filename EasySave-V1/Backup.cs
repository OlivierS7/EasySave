using System;
using System.IO;

namespace Save
{
    public class Backup
    {

        private string backupName;
        private string srcDirectoy;
        private DirectoryInfo source;
        private string destDirectory;
        private DirectoryInfo dest;
        private int backupType;
        /* public Backup(string backupName, string srcDirectory, string destDirectory, int backupType)
        {
            this.backupName = backupName;
            this.srcDirectoy = srcDirectory;
            this.destDirectory = destDirectory;
            this.backupType = backupType;
        } */

        public Backup() { }

        public string GetbackupName()
        {
            return backupName;
        }

        public void SetbackupName(string backupName)
        {
            this.backupName = backupName;
        }

        public string GetsrcDirectory()
        {
            return srcDirectoy;
        }

        public void SetsrcDirectory(string srcDirectory)
        {
            this.srcDirectoy = srcDirectory;
            SetsrcDirectoryInfo(srcDirectory);
        }

        public string GetdestDirectory()
        {
            return destDirectory;
        }

        public void SetdestDirectory(string destDirectory)
        {
            this.destDirectory = destDirectory;
            SetdestDirectoryInfo(destDirectory);
        }
        public DirectoryInfo GetsrcDirectoryInfo()
        {
            return source;
        }

        public void SetsrcDirectoryInfo(string srcDirectory)
        {
            this.source = new DirectoryInfo(srcDirectory);
        }
        public DirectoryInfo GetdestDirectoryInfo()
        {
            return dest;
        }

        public void SetdestDirectoryInfo(string destDirectory)
        {
            this.dest = new DirectoryInfo(destDirectory);
        }

        public int GetbackupType()
        {
            return backupType;
        }

        public void SetbackupType(int backupType)
        {
            this.backupType = backupType;
        }

        public void Copy()
        {
            if(this.backupType == 1)
            {
                NewSaveDirectory();
                Console.WriteLine(GetdestDirectory() + "\\Backup");
                CopyAll(GetsrcDirectoryInfo(), GetdestDirectoryInfo());
            } else if (this.backupType == 2){
                Console.WriteLine("sauvegarde incrémentielle");
            } else
            {
                Console.WriteLine("Mauvaise sauvegarde relancer le programme");
            }

        }

        public void NewSaveDirectory()
        {
            String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            String TodaysTime = DateTime.Now.ToString("HH-mm");
            if (!Directory.Exists(GetdestDirectory()))
            {
                GetdestDirectoryInfo().CreateSubdirectory("Backup - " + Todaysdate + "_" + TodaysTime);
                SetdestDirectoryInfo(GetdestDirectory() + "\\Backup - " + Todaysdate + "_" + TodaysTime);
            } else
            {
                Directory.CreateDirectory(GetdestDirectory());
                GetdestDirectoryInfo().CreateSubdirectory("Backup - " + Todaysdate + "_" + TodaysTime);
                SetdestDirectoryInfo(GetdestDirectory() + "\\Backup - " + Todaysdate + "_" + TodaysTime);
            }
        }
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