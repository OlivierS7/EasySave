using System;
using System.IO;

namespace NSModel {
	public class FullSave : SaveStrategy  {
		public void LastFullSaveForSrcDir(string srcDir, string destDir) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Execute(SaveTemplate template) {
            String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            String TodaysTime = DateTime.Now.ToString("HH-mm-ss");
            if (!Directory.Exists(template.destDirectory))
            {
                // Create SubDirectory with date and time
                Console.WriteLine(template.destDirectoryInfo);
                template.destDirectoryInfo.CreateSubdirectory(Todaysdate + "_" + TodaysTime);
                template.destDirectoryInfo = new DirectoryInfo(template.destDirectoryInfo + "\\" + Todaysdate + "_" + TodaysTime);
            }
            else
            {              
                // Create Directory and SubDirectory with date and time
                Directory.CreateDirectory(template.destDirectory);
                template.destDirectoryInfo.CreateSubdirectory(Todaysdate + "_" + TodaysTime);
                template.destDirectoryInfo = new DirectoryInfo(template.destDirectory + "\\" + Todaysdate + "_" + TodaysTime);
            };
            CopyAll(template.srcDirectoryInfo, template.destDirectoryInfo);
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
