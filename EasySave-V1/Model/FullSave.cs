using System;
using System.Diagnostics;
using System.IO;
using NSModel.Singleton;

namespace NSModel {
	public class FullSave : SaveStrategy  {
		public void LastFullSaveForSrcDir(string srcDir, string destDir) {
			throw new System.NotImplementedException("Not implemented");
		}

		public void Execute(SaveTemplate template) {
            String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            String TodaysTime = DateTime.Now.ToString("HH-mm-ss");
            Stopwatch stopw = new Stopwatch();
            stopw.Start();
            if (!Directory.Exists(template.destDirectory))
            {                
                // Create Directory and SubDirectory with date and time
                Directory.CreateDirectory(template.destDirectory);
                template.destDirectoryInfo.CreateSubdirectory(Todaysdate + "_" + TodaysTime);
                template.destDirectoryInfo = new DirectoryInfo(template.destDirectoryInfo + "\\" + Todaysdate + "_" + TodaysTime);
            }
            else
            {
                // Create SubDirectory with date and time
                template.destDirectoryInfo.CreateSubdirectory(Todaysdate + "_" + TodaysTime);
                template.destDirectoryInfo = new DirectoryInfo(template.destDirectory + "\\" + Todaysdate + "_" + TodaysTime);
            }
            
            CopyAll(template.srcDirectoryInfo, template.destDirectoryInfo);
            stopw.Stop();
            Log.GetLogInstance().Write(template,DirSize(template.srcDirectoryInfo), stopw.Elapsed);
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
        public long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
