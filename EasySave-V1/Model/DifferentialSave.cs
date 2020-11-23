using System;
using System.IO;
using NSModel.Singleton;

namespace NSModel
{
	public class DifferentialSave : SaveStrategy  {
		public SaveTemplate CheckFullSave(SaveTemplate template) {
			return FullSaveHistory.GetInstance().GetFullSaveForDir(template);
        }
        public void Execute(SaveTemplate template)
        {
            SaveTemplate fullSave = CheckFullSave(template);
            if (fullSave != null)
            {
                Console.WriteLine(fullSave.destDirectory);
                String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
                String TodaysTime = DateTime.Now.ToString("HH-mm-ss");
                DirectoryInfo srcDirectoryInfo = new DirectoryInfo(template.srcDirectory);
                DirectoryInfo destDirectoryInfo = new DirectoryInfo(template.destDirectory);
                if (!Directory.Exists(template.destDirectory))
                {
                    /* Create Directory and SubDirectory with date and time */
                    Directory.CreateDirectory(template.destDirectory);
                    destDirectoryInfo.CreateSubdirectory(Todaysdate + "_" + TodaysTime);
                    destDirectoryInfo = new DirectoryInfo(destDirectoryInfo + "\\" + Todaysdate + "_" + TodaysTime);
                }
                else
                {
                    /* Create SubDirectory with date and time */
                    destDirectoryInfo.CreateSubdirectory(Todaysdate + "_" + TodaysTime);
                    destDirectoryInfo = new DirectoryInfo(template.destDirectory + "\\" + Todaysdate + "_" + TodaysTime);
                };
                CopyAll(srcDirectoryInfo, destDirectoryInfo);
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
