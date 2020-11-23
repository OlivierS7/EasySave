using System;
using System.Diagnostics;
using System.IO;
using NSModel.Singleton;

namespace NSModel {
	public class FullSave : SaveStrategy  {

        /* Method to execute a backup */
        public void Execute(SaveTemplate template) {
            String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            String TodaysTime = DateTime.Now.ToString("HH-mm-ss");
            string dateTime = Todaysdate + "_" + TodaysTime;
            DirectoryInfo srcDirectoryInfo = new DirectoryInfo(template.srcDirectory);
            DirectoryInfo destDirectoryInfo = new DirectoryInfo(template.destDirectory);
            if (!Directory.Exists(template.destDirectory))
            {                
                /* Create Directory and SubDirectory with date and time */
                Directory.CreateDirectory(template.destDirectory);
                destDirectoryInfo.CreateSubdirectory(dateTime);
                destDirectoryInfo = new DirectoryInfo(destDirectoryInfo + "\\" + dateTime);
            }
            else
            {
                /* Create SubDirectory with date and time */
                destDirectoryInfo.CreateSubdirectory(dateTime);
                destDirectoryInfo = new DirectoryInfo(template.destDirectory + "\\" + dateTime);
            };
            CopyAll(srcDirectoryInfo, destDirectoryInfo, template.backupName);
            /* Call the Singleton to write in FullSaveHistory.json */
            FullSaveHistory.GetInstance().Write(template, dateTime);
        }

        /* Method to create a full backup of a directory */
        public void CopyAll(DirectoryInfo source, DirectoryInfo target, string saveTemplateName)
        {
            Stopwatch stopw = new Stopwatch();
            Directory.CreateDirectory(target.FullName);
            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                stopw.Start();
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                stopw.Stop();
                Log.GetLogInstance().Write(saveTemplateName, fi, new FileInfo(Path.Combine(target.FullName, fi.Name)), fi.Length, stopw.Elapsed);
                stopw.Reset();
            }
            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, saveTemplateName);
            }
        }
    }
}
