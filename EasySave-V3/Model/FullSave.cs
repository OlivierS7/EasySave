using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using EasySave_V3.Properties;
using NSModel.Singleton;

namespace NSModel
{
    public class FullSave : SaveStrategy
    {
        private delegate void deleg();
        private long sizeLeft;
        private int filesLeft;
        private string cryptDuration;
        private DateTime currentDateTime;
        private int totalFiles;
        private long totalSize;
        private float progression;
        private bool abort = false;
        private string status;
        private ManualResetEvent mre = new ManualResetEvent(true);
        private SaveTemplate template;
        public event SaveStrategy.TemplateStatusDelegate refreshStatusDelegate;
        public event SaveStrategy.TemplateProgressDelegate refreshProgressDelegate;
        private Mutex updateProgress = new Mutex();
        private int threadsRunning;
        private bool aborted;

        public string PauseOrResume(bool play)
        {
            if (play)
            {
                mre.Set();
                UpdateStatus(Resources.Running);
                return status;
            }
            else
            {
                mre.Reset();
                UpdateStatus(Resources.Paused);
            }
            return status;
        }

        public void AbortExecution(bool isAbort)
        {
            abort = isAbort;
        }
        /* Method to execute a backup */
        public void Execute(SaveTemplate template, List<string> extensionsToEncrypt)
        {
            aborted = false;
            threadsRunning = 0;
            this.template = template;
            UpdateStatus(Resources.Running);
            List<string> priorityExtensions = SaveParameter.GetInstance().Parameters1.getPriorityFilesExtensions();
            List<string> priorityFiles = new List<string>();
            List<string> normalFiles = new List<string>();
            abort = false;
            /* Variable for the directory name */
            currentDateTime = DateTime.Now;
            string Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            string TodaysTime = DateTime.Now.ToString("HH-mm-ss");
            string dateTimeName = Todaysdate + "_" + TodaysTime + "_" + template.backupName;
            Stopwatch stopw = new Stopwatch();

            string[] allFiles = Directory.GetFiles(template.srcDirectory, ".", SearchOption.AllDirectories);

            /* Couting total files and their size */
            totalFiles = allFiles.Length;
            filesLeft = totalFiles;
            totalSize = 0;
            bool priority = false;
            foreach (string currentFile in allFiles)
            {
                foreach (string priorityExtension in priorityExtensions)
                {
                    if (Path.GetExtension(currentFile) == priorityExtension)
                    {
                        priorityFiles.Add(currentFile);
                        priority = true;
                    }
                }
                if (!priority)
                    normalFiles.Add(currentFile);
                FileInfo info = new FileInfo(currentFile);
                totalSize += info.Length;
                priority = false;
            }
            sizeLeft = totalSize;

            /* Initializing state file */
            State.GetInstance().Write(currentDateTime, template, true, null, null, 0, totalSize, totalSize, totalFiles, totalFiles, TimeSpan.Zero);

            Stopwatch totalTime = new Stopwatch();
            totalTime.Start();
            DirectoryInfo destDirectoryInfo = new DirectoryInfo(template.destDirectory);
            if (!Directory.Exists(template.destDirectory))
            {
                /* Create Directory and SubDirectory with date and time */
                Directory.CreateDirectory(template.destDirectory);
                destDirectoryInfo.CreateSubdirectory(dateTimeName);
                destDirectoryInfo = new DirectoryInfo(destDirectoryInfo + "\\" + dateTimeName);
            }
            else
            {
                /* Create SubDirectory with date and time */
                destDirectoryInfo.CreateSubdirectory(dateTimeName);
                destDirectoryInfo = new DirectoryInfo(template.destDirectory + "\\" + dateTimeName);
            };
            Model.Barrier.SignalAndWait();
            mre.WaitOne();
            
            if (!abort) 
            {
                Model.SetPriority(true);
                Model.IncreasePrioritySaves();
                copyPerGroup(priorityFiles, template, destDirectoryInfo, extensionsToEncrypt, stopw, totalTime, true);
                Model.DecreasePrioritySaves();
                CheckEnd(destDirectoryInfo);
                Model.Barrier.SignalAndWait();
                mre.WaitOne();
            }

            if (!abort)
            {
                while (Model.GetPriority())
                {
                    Thread.Sleep(1000);
                }
                copyPerGroup(normalFiles, template, destDirectoryInfo, extensionsToEncrypt, stopw, totalTime, false);
                /* Call the Singleton to write in FullSaveHistory.json */
                FullSaveHistory.GetInstance().Write(template, dateTimeName);
                State.GetInstance().Write(currentDateTime, template, false, null, null, 0, totalSize, 0, totalFiles, 0, totalTime.Elapsed);
                totalTime.Stop();
            }
            CheckEnd(destDirectoryInfo);
            abort = false;
        }

        /* Method to create a full backup of a directory */
        public void Copy(string srcDir, string destDir, Stopwatch stopw, FileInfo src, List<string> extensionsToEncrypt)
        {
            /* Creating files and folders if they doesn't exists */
            new FileInfo(src.FullName.Replace(srcDir, destDir)).Directory.Create();
            stopw.Start();
            bool isCrypted = false;
            foreach (string extension in extensionsToEncrypt)
            {
                if (Path.GetExtension(src.ToString()) == extension)
                {
                    isCrypted = true;
                    cryptDuration = crypt(src.ToString(), src.FullName.Replace(srcDir, destDir));
                }
            }

            if (!isCrypted)
                src.CopyTo(src.FullName.Replace(srcDir, destDir));
            filesLeft--;
            sizeLeft = sizeLeft - src.Length;
            stopw.Stop();
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
        public void copyPerGroup(List<string> files, SaveTemplate template, DirectoryInfo destDirectoryInfo, List<string> extensionsToEncrypt, Stopwatch stopw, Stopwatch totalTime, bool priority)
        {
            foreach (string file in files)
            {
                if (!abort)
                {
                    mre.WaitOne();
                    cryptDuration = "0";
                    FileInfo src = new FileInfo(file);
                    string srcDir = template.srcDirectory;
                    string destDir = destDirectoryInfo.FullName;
                    if (src.Length/1000 > Model.getMaxFileSize())
                    {
                        deleg delg = () =>
                        {
                            if (priority)
                                Model.IncreasePrioritySaves();
                            threadsRunning++;
                            Model.Mutex.WaitOne();
                            if (!abort)
                            {
                                mre.WaitOne();
                                Stopwatch largeFileStopw = new Stopwatch();
                                Copy(template.srcDirectory, destDirectoryInfo.FullName, largeFileStopw, src, extensionsToEncrypt);
                                UpdateProgress(sizeLeft, totalSize);
                                State.GetInstance().Write(currentDateTime, template, true, src.FullName, src.FullName.Replace(srcDir, destDir), src.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                                Log.GetInstance().Write(template.backupName, src, new FileInfo(src.FullName.Replace(srcDir, destDir)), src.Length, largeFileStopw.Elapsed, cryptDuration);
                                largeFileStopw.Reset();
                            }
                            if (priority)
                                Model.DecreasePrioritySaves();
                            threadsRunning--;
                            CheckEnd(destDirectoryInfo);
                            Model.Mutex.ReleaseMutex();
                        };
                        Thread largeFile = new Thread(new ThreadStart(delg));
                        largeFile.Start();
                    }
                    else
                    {
                        threadsRunning++;
                        if (priority)
                            Model.IncreasePrioritySaves();
                        Copy(template.srcDirectory, destDirectoryInfo.FullName, stopw, src, extensionsToEncrypt);
                        UpdateProgress(sizeLeft, totalSize);
                        State.GetInstance().Write(currentDateTime, template, true, src.FullName, src.FullName.Replace(srcDir, destDir), src.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                        Log.GetInstance().Write(template.backupName, src, new FileInfo(src.FullName.Replace(srcDir, destDir)), src.Length, stopw.Elapsed, cryptDuration);
                        stopw.Reset();
                        if (priority)
                            Model.DecreasePrioritySaves();
                        threadsRunning--;
                        CheckEnd(destDirectoryInfo);
                    }
                }
                else
                    break;
            }
        }

        public void UpdateStatus(string status)
        {
            this.status = status;
            refreshStatusDelegate?.Invoke(status);
        }
        public void UpdateProgress(long sizeLeft, long totalSize)
        {
            updateProgress.WaitOne();
            if (totalSize == 0)
                progression = 0;
            else
                progression = 100 - ((sizeLeft * 100) / totalSize);
            updateProgress.ReleaseMutex();
            refreshProgressDelegate?.Invoke(progression);
        }

        public string getStatus()
        {
            return this.status;
        }
        private void CheckEnd(DirectoryInfo destDirectoryInfo)
        {
            if (Model.GetPrioritySaves() == 0)
                Model.SetPriority(false);
            if (abort && threadsRunning == 0 && !aborted)
            {
                Model.RemoveThread(template);
                UpdateProgress(0, 0);
                State.Mutex.WaitOne();
                Log.Mutex.WaitOne();
                destDirectoryInfo.Delete(true);
                State.Mutex.ReleaseMutex();
                Log.Mutex.ReleaseMutex();
                UpdateStatus(Resources.Aborted);
                aborted = true;
            }
            else if (filesLeft == 0)
            {
                Model.RemoveThread(template);
                UpdateStatus(Resources.Finished);
            }
        }
    }
}
