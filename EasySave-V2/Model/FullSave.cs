using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NSModel.Singleton;

namespace NSModel
{
    public class FullSave : SaveStrategy
    {
        private int filesLeft;

        /* Method to execute a backup */
        public void Execute(SaveTemplate template, List<string> extensionsToEncrypt)
        {

            /* Variable for the directory name */
            DateTime currentDateTime = DateTime.Now;
            string Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            string TodaysTime = DateTime.Now.ToString("HH-mm-ss");
            string dateTime = Todaysdate + "_" + TodaysTime;

            string[] allFiles = Directory.GetFiles(template.srcDirectory, ".", SearchOption.AllDirectories);

            /* Couting total files and their size */
            int totalFiles = allFiles.Length;
            filesLeft = totalFiles;
            long totalSize = 0;
            foreach (string currentFile in allFiles)
            {
                FileInfo info = new FileInfo(currentFile);
                totalSize += info.Length;
            }

            /* Initializing state file */
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
            CopyAll(srcDirectoryInfo, destDirectoryInfo, template.backupName, totalTime, currentDateTime, template, totalFiles, totalSize, extensionsToEncrypt);
            /* Call the Singleton to write in FullSaveHistory.json */
            FullSaveHistory.GetInstance().Write(template, dateTime);
            State.GetInstance().Write(currentDateTime, template, false, null, null, 0, totalSize, 0, totalFiles, 0, totalTime.Elapsed);
            totalTime.Stop();
        }

        /* Method to create a full backup of a directory */
        public void CopyAll(DirectoryInfo source, DirectoryInfo target, string saveTemplateName, Stopwatch totalTime, DateTime start, SaveTemplate template, int totalFiles, long totalSize, List<string> extensionsToEncrypt)
        {
            Stopwatch stopw = new Stopwatch();
            Directory.CreateDirectory(target.FullName);
            long sizeLeft = totalSize;
            string cryptDuration;
            bool isCrypted;
            string destination;
            string sourceFile;
            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                cryptDuration = "0";
                isCrypted = false;
                destination = Path.Combine(target.FullName, fi.Name);
                sourceFile = fi.ToString();
                stopw.Start();

                /* Checking if file needs to be crypted */
                foreach (string extension in extensionsToEncrypt)
                {
                    if (Path.GetExtension(sourceFile) == extension)
                    {
                        isCrypted = true;
                        cryptDuration = crypt(sourceFile, destination);
                    }
                    
                }

                /* If file doesn't need to be crypted, simply copy it */
                if (!isCrypted)
                    fi.CopyTo(destination, true);
                filesLeft--;
                sizeLeft = sizeLeft - fi.Length;
                State.GetInstance().Write(start, template, true, sourceFile, destination, fi.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                stopw.Stop();
                Log.GetInstance().Write(saveTemplateName, fi, new FileInfo(destination), fi.Length, stopw.Elapsed, cryptDuration);
                stopw.Reset();
            }
            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, saveTemplateName, totalTime, start, template, totalFiles, totalSize, extensionsToEncrypt);
            }
        }
        public string crypt(string sourceFile, string destination)
        {
            /* Creating a new process */
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (File.Exists(@".\..\..\..\..\CryptoSoft\CryptoSoft\bin\Release\netcoreapp3.1\CryptoSoft.exe"))
                startInfo.FileName = @".\..\..\..\..\CryptoSoft\CryptoSoft\bin\Release\netcoreapp3.1\CryptoSoft.exe";
            else
                startInfo.FileName = @"CryptoSoft.exe";
            startInfo.ArgumentList.Add(sourceFile);
            startInfo.ArgumentList.Add(destination);
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            Process currentProcess = Process.Start(startInfo);
            currentProcess.WaitForExit();

            /* Returning exit code of process which is the crypt duration */
            return currentProcess.ExitCode.ToString();
        }
    }
}
