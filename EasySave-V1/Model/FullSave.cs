using System;
using System.Diagnostics;
using System.IO;
using NSModel.Singleton;

namespace NSModel {
	public class FullSave : SaveStrategy  {

        /* Method to execute a backup */
        public void Execute(SaveTemplate template) {
            DateTime currentDateTime = DateTime.Now;
            string Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            string TodaysTime = DateTime.Now.ToString("HH-mm-ss");
            string dateTime = Todaysdate + "_" + TodaysTime;
            string[] allFiles = Directory.GetFiles(template.srcDirectory, ".", SearchOption.AllDirectories);
            int totalFiles = allFiles.Length;
            long totalSize = 0;
            foreach (string currentFile in allFiles)
            {
                FileInfo info = new FileInfo(currentFile);
                totalSize += info.Length;
            }
            State.GetInstance().Write(currentDateTime, template, true, null, null, 0, totalSize, totalSize, totalFiles, totalFiles, TimeSpan.Zero);
            Stopwatch totalTime = new Stopwatch();
            totalTime.Start();
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
            CopyAll(srcDirectoryInfo, destDirectoryInfo, template.backupName, totalTime, currentDateTime, template, totalFiles, totalSize);
            /* Call the Singleton to write in FullSaveHistory.json */
            FullSaveHistory.GetInstance().Write(template, dateTime);
            State.GetInstance().Write(currentDateTime, template, false, null, null, 0, totalSize, 0, totalFiles, 0, totalTime.Elapsed);
            totalTime.Stop();
        }

        /* Method to create a full backup of a directory */
        public void CopyAll(DirectoryInfo source, DirectoryInfo target, string saveTemplateName, Stopwatch totalTime, DateTime start, SaveTemplate template, int totalFiles, long totalSize)
        {
            Stopwatch stopw = new Stopwatch();
            Directory.CreateDirectory(target.FullName);
            int filesLeft = totalFiles;
            long sizeLeft = totalSize;
            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                stopw.Start();
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                filesLeft--;
                sizeLeft = sizeLeft - fi.Length;
                State.GetInstance().Write(start, template, true, fi.ToString(), Path.Combine(target.FullName, fi.Name), fi.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                stopw.Stop();
                Log.GetInstance().Write(saveTemplateName, fi, new FileInfo(Path.Combine(target.FullName, fi.Name)), fi.Length, stopw.Elapsed);
                stopw.Reset();
            }
            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, saveTemplateName, totalTime, start, template, totalFiles, totalSize);
            }
        }
    }
}
