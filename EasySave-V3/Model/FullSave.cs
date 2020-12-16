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
        string dateTimeName;
        private Mutex checkEnd = new Mutex();

        /* Method to pause or resume current save */
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

        /* Method to abort current save */
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
            dateTimeName = Todaysdate + "_" + TodaysTime + "_" + template.backupName;
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
            /* Synchronize barrier */
            Model.Barrier.SignalAndWait();
            /* Resent event to pause execution */
            mre.WaitOne();

            /* Checking if save needs to abort */
            if (!abort) 
            {
                /* Transfer priority files */
                Model.SetPriority(true);
                Model.IncreasePrioritySaves();
                copyPerGroup(priorityFiles, template, destDirectoryInfo, extensionsToEncrypt, stopw, totalTime, true);
                Model.DecreasePrioritySaves();
                CheckEnd(destDirectoryInfo, totalTime.Elapsed);
                /* Synchronize barrier */
                Model.Barrier.SignalAndWait();
                /* Resent event to pause execution */
                mre.WaitOne();
            }

            /* Checking if save needs to abort */
            if (!abort)
            {
                /* Waiting for priority files to transfer */
                while (Model.GetPriority())
                {
                    Thread.Sleep(1000);
                }
                /* Transfer normal files */
                copyPerGroup(normalFiles, template, destDirectoryInfo, extensionsToEncrypt, stopw, totalTime, false);
                /* Call the Singleton to write in FullSaveHistory.json */
                totalTime.Stop();
            }
            CheckEnd(destDirectoryInfo, totalTime.Elapsed);
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
                /* Checking if save needs to abort */
                if (!abort)
                {
                    /* Resent event to pause execution */
                    mre.WaitOne();
                    cryptDuration = "0";
                    FileInfo src = new FileInfo(file);
                    string srcDir = template.srcDirectory;
                    string destDir = destDirectoryInfo.FullName;

                    /* Checking for large file */
                    if (src.Length / 1000 > Model.getMaxFileSize())
                    {
                        deleg delg = () =>
                        {
                            /* Checking if it's priority work */
                            if (priority)
                                Model.IncreasePrioritySaves();
                            threadsRunning++;

                            /* Allows only one large file to be transfered at a time */
                            Model.Mutex.WaitOne();
                            /* Checking if save needs to abort */
                            if (!abort)
                            {
                                /* Resent event to pause execution */
                                mre.WaitOne();
                                Stopwatch largeFileStopw = new Stopwatch();

                                /* Copy a file */
                                Copy(template.srcDirectory, destDirectoryInfo.FullName, largeFileStopw, src, extensionsToEncrypt);
                                UpdateProgress(sizeLeft, totalSize);
                                State.GetInstance().Write(currentDateTime, template, true, src.FullName, src.FullName.Replace(srcDir, destDir), src.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                                Log.GetInstance().Write(template.backupName, src, new FileInfo(src.FullName.Replace(srcDir, destDir)), src.Length, largeFileStopw.Elapsed, cryptDuration);
                                largeFileStopw.Reset();
                            }
                            /* Checking if it's priority work */
                            if (priority)
                                Model.DecreasePrioritySaves();
                            threadsRunning--;
                            CheckEnd(destDirectoryInfo, totalTime.Elapsed);
                            Model.Mutex.ReleaseMutex();
                        };
                        Thread largeFile = new Thread(new ThreadStart(delg));
                        largeFile.Start();
                    }
                    else
                    {
                        threadsRunning++;
                        /* Checking if it's priority work */
                        if (priority)
                            Model.IncreasePrioritySaves();

                        /* Copy a file */
                        Copy(template.srcDirectory, destDirectoryInfo.FullName, stopw, src, extensionsToEncrypt);
                        UpdateProgress(sizeLeft, totalSize);
                        State.GetInstance().Write(currentDateTime, template, true, src.FullName, src.FullName.Replace(srcDir, destDir), src.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                        Log.GetInstance().Write(template.backupName, src, new FileInfo(src.FullName.Replace(srcDir, destDir)), src.Length, stopw.Elapsed, cryptDuration);
                        stopw.Reset();
                        /* Checking if it's priority work */
                        if (priority)
                            Model.DecreasePrioritySaves();
                        threadsRunning--;
                        CheckEnd(destDirectoryInfo, totalTime.Elapsed);
                    }
                }
                else
                    break;
            }
        }

        /* Method to update progress of the running save */
        public void UpdateProgress(long sizeLeft, long totalSize)
        {
            updateProgress.WaitOne();
            if (totalSize == 0)
                this.progression = 0;
            else
                this.progression = 100 - ((sizeLeft * 100) / totalSize);
            updateProgress.ReleaseMutex();
            refreshProgressDelegate?.Invoke(progression);
        }

        /* Method to update status of the running save */
        public void UpdateStatus(string status)
        {
            this.status = status;
            refreshStatusDelegate?.Invoke(status);
        }
        public string getStatus()
        {
            return this.status;
        }

        /* Method to check for end of priority files and for end of current save */
        private void CheckEnd(DirectoryInfo destDirectoryInfo, TimeSpan totalTime)
        {
            checkEnd.WaitOne();
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
                FullSaveHistory.GetInstance().Write(template, dateTimeName);
                State.GetInstance().Write(currentDateTime, template, false, null, null, 0, totalSize, 0, totalFiles, 0, totalTime);
            }
            checkEnd.ReleaseMutex();
        }
    }
}
