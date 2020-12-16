using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NSModel.Singleton;
using EasySave_V3.Properties;

namespace NSModel
{
    public class DifferentialSave : SaveStrategy
    {
        private delegate void deleg();
        bool wasCreated = false;
        bool sameFile;
        string currentSrcDirName;
        string cryptDuration;
        FileInfo comp;
        DirectoryInfo currentCompDir;
        DirectoryInfo currentSrcDir;
        int filesLeft;
        long sizeLeft;
        List<string> priorityExtensions = new List<string>();
        List<string> priorityFiles = new List<string>();
        List<string> normalFiles = new List<string>();
        private DateTime currentDateTime;
        private int totalFiles;
        private long totalSize;
        private bool abort = false;
        private string status;
        private ManualResetEvent mre = new ManualResetEvent(true);
        private SaveTemplate template;
        private float progression;
        private Mutex updateProgress = new Mutex();
        private int threadsRunning;
        private bool aborted;

        public event SaveStrategy.TemplateStatusDelegate refreshStatusDelegate;
        public event SaveStrategy.TemplateProgressDelegate refreshProgressDelegate;

        public DifferentialSave()
        {
            UpdateStatus(Resources.Ready);
        }
        public string PauseOrResume(bool play)
        {
            if (play)
            {
                mre.Set();
                UpdateStatus(Resources.Running);
                return status;
            }
            else
                mre.Reset();
            UpdateStatus(Resources.Paused);
            return status;
        }

        public void AbortExecution(bool isAbort)
        {
            abort = isAbort;
            UpdateStatus(Resources.Ready);
        }
        public SaveTemplate CheckFullSave(SaveTemplate template)
        {
            return FullSaveHistory.GetInstance().GetFullSaveForDir(template);
        }
        public void Execute(SaveTemplate template, List<string> extensionsToEncrypt)
        {
            threadsRunning = 0;
            this.template = template;
            UpdateStatus(Resources.Running);
            Model.IncreasePrioritySaves();
            SaveTemplate fullSave = CheckFullSave(template);
            if (fullSave != null)
            {
                /* Variables to get the current time for the directory name */
                currentDateTime = DateTime.Now;
                String Todaysdate = currentDateTime.ToString("dd-MMM-yyyy");
                String TodaysTime = currentDateTime.ToString("HH-mm-ss");
                string dateTimeName = Todaysdate + "_" + TodaysTime + "_" + template.backupName;

                /* Variables for the source and destination directories + the directory to compare with for the differential save */
                string destDir = template.destDirectory + "\\" + dateTimeName;
                DirectoryInfo destDirectoryInfo = new DirectoryInfo(destDir);
                string srcDir = template.srcDirectory;
                string compDir = fullSave.destDirectory;
                string[] srcFiles = Directory.GetFiles(srcDir, ".", SearchOption.AllDirectories);
                string[] compFiles = Directory.GetFiles(compDir, ".", SearchOption.AllDirectories);

                long[] countFiles = CountFiles(srcFiles, compFiles, srcDir, compDir);
                totalFiles = unchecked((int)countFiles[0]);
                totalSize = countFiles[1];
                filesLeft = totalFiles;
                sizeLeft = totalSize;
                Stopwatch stopw = new Stopwatch();

                /* Initialize state file */
                State.GetInstance().Write(currentDateTime, template, true, null, null, 0, totalSize, totalSize, totalFiles, totalFiles, TimeSpan.Zero);
                Stopwatch totalTime = new Stopwatch();
                totalTime.Start();
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
        public long[] CountFiles(string[] srcFiles, string[] compFiles, string srcDir, string compDir)
        {
            bool priority = false;
            priorityExtensions = SaveParameter.GetInstance().Parameters1.getPriorityFilesExtensions();
            int totalFiles = 0;
            long totalSize = 0;

            /* Couting number of files to copy and total size to transfer */
            foreach (string srcFile in srcFiles)
            {
                FileInfo src = new FileInfo(srcFile);
                currentSrcDirName = src.DirectoryName;
                currentSrcDir = src.Directory;
                foreach (string compFile in compFiles)
                {
                    sameFile = false;
                    comp = new FileInfo(compFile);
                    currentCompDir = comp.Directory;
                    if (currentSrcDirName != srcDir)
                    {
                        if (currentSrcDir.Name == currentCompDir.Name)
                            sameFile = true;
                    }
                    if (currentSrcDirName == srcDir)
                    {
                        if (currentSrcDirName.EndsWith(currentSrcDir.Name))
                        {
                            if (currentCompDir.FullName.EndsWith(compDir))
                                sameFile = true;
                        }
                    }
                    /* Checking if files have the same name */
                    if (comp.Name == src.Name && sameFile)
                    {
                        wasCreated = true;

                        /* Checking if the file has been modified */
                        if (comp.Length != src.Length)
                        {
                            foreach (string priorityExtension in priorityExtensions)
                            {
                                if (Path.GetExtension(srcFile) == priorityExtension)
                                {
                                    priorityFiles.Add(srcFile);
                                    priority = true;
                                }
                            }
                            totalSize += src.Length;
                            totalFiles++;
                        }
                    }
                }
                /* If file exists only in the src directory, it was created recently */
                if (!wasCreated)
                {
                    foreach (string priorityExtension in priorityExtensions)
                    {
                        if (Path.GetExtension(srcFile) == priorityExtension)
                        {
                            priorityFiles.Add(srcFile);
                            priority = true;
                        }
                    }
                    totalSize += src.Length;
                    totalFiles++;
                    if (!priority)
                        normalFiles.Add(srcFile);
                }
                wasCreated = false;
                priority = false;
            }
            long[] results = { totalFiles, totalSize };
            return results;
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
                    if (src.Length / 1000 > Model.getMaxFileSize())
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

        public void UpdateStatus(string status)
        {
            this.status = status;
            refreshStatusDelegate?.Invoke(status);
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
