using System;
using System.Diagnostics;
using System.IO;
using NSModel.Singleton;

namespace NSModel
{
    public class DifferentialSave : SaveStrategy
    {
        public SaveTemplate CheckFullSave(SaveTemplate template)
        {
            return FullSaveHistory.GetInstance().GetFullSaveForDir(template);
        }
        public void Execute(SaveTemplate template)
        {
            SaveTemplate fullSave = CheckFullSave(template);
            if (fullSave != null)
            {
                /* Variables to get the current time for the directory name */
                String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
                String TodaysTime = DateTime.Now.ToString("HH-mm-ss");
                string dateTime = Todaysdate + "_" + TodaysTime;
                DateTime currentDateTime = DateTime.Now;

                /* Variables for the source and destination directories + the directory to compare with for the differential save */
                string destDir = template.destDirectory + "\\" + dateTime;
                string srcDir = template.srcDirectory;
                string compDir = fullSave.destDirectory;
                string[] srcFiles = Directory.GetFiles(srcDir, ".", SearchOption.AllDirectories);
                string[] compFiles = Directory.GetFiles(compDir, ".", SearchOption.AllDirectories);
                DirectoryInfo srcDirInfo = new DirectoryInfo(srcDir);

                bool wasCreated = false;
                bool sameFile = false;
                int totalFiles = 0;
                long totalSize = 0;
                int filesLeft = totalFiles;
                long sizeLeft = totalSize;

                foreach (string srcFile in srcFiles)
                {
                    FileInfo src = new FileInfo(srcFile);
                    foreach (string compFile in compFiles)
                    {
                        FileInfo comp = new FileInfo(compFile);
                        /* Checking if files have the same name */
                        if (comp.Name == src.Name)
                        {
                            wasCreated = true;

                            /* Checking if the file has been modified */
                            if (comp.Length != src.Length)
                            {
                                totalSize += src.Length;
                                totalFiles++;
                            }
                        }
                    }
                    /* If file exists only in the src directory, it was created recently */
                    if (!wasCreated)
                    {
                        totalSize += src.Length;
                        totalFiles++;
                    }
                    wasCreated = false;
                }
                State.GetInstance().Write(currentDateTime, template, true, null, null, 0, totalSize, totalSize, totalFiles, totalFiles, TimeSpan.Zero);
                Stopwatch totalTime = new Stopwatch();
                totalTime.Start();

                Stopwatch stopw = new Stopwatch();
                string cryptDuration = "0";
                foreach (string srcFile in srcFiles)
                {
                    FileInfo src = new FileInfo(srcFile);
                    string currentSrcDirName = src.DirectoryName;
                    DirectoryInfo currentSrcDir = src.Directory;
                    foreach (string compFile in compFiles)
                    {
                        sameFile = false;
                        FileInfo comp = new FileInfo(compFile);
                        DirectoryInfo currentCompDir = comp.Directory;
                        if (currentSrcDirName != srcDir)
                        {
                            if (currentSrcDir.Name == currentCompDir.Name)
                            {
                                sameFile = true;
                            }
                        }
                        if (currentSrcDirName == srcDir)
                        {
                            if (currentSrcDirName.EndsWith(currentSrcDir.Name))
                            {
                                if (currentCompDir.FullName.EndsWith(compDir))
                                {
                                    sameFile = true;
                                }
                            }
                        }
                        /* Checking if files have the same name */
                        if (comp.Name == src.Name && sameFile)
                        {
                            wasCreated = true;

                            /* Checking if the file has been modified */
                            if (comp.Length != src.Length)
                            {
                                /* Creating files and folders if they doesn't exists */
                                new FileInfo(src.FullName.Replace(srcDir, destDir)).Directory.Create();
                                stopw.Start();
                                if (Path.GetExtension(src.ToString()) == ".txt")
                                {
                                    ProcessStartInfo startInfo = new ProcessStartInfo();
                                    if (File.Exists(@".\..\..\..\..\CryptoSoft\CryptoSoft\bin\Release\netcoreapp3.1\CryptoSoft.exe"))
                                    {
                                        startInfo.FileName = @".\..\..\..\..\CryptoSoft\CryptoSoft\bin\Release\netcoreapp3.1\CryptoSoft.exe";
                                    }
                                    else
                                    {
                                        startInfo.FileName = @"CryptoSoft.exe";
                                    }
                                    startInfo.ArgumentList.Add(src.ToString());
                                    startInfo.ArgumentList.Add(src.FullName.Replace(srcDir, destDir));
                                    startInfo.UseShellExecute = false;
                                    startInfo.RedirectStandardOutput = true;
                                    startInfo.CreateNoWindow = true;
                                    Process currentProcess = Process.Start(startInfo);
                                    currentProcess.WaitForExit();
                                    cryptDuration = currentProcess.ExitCode.ToString();
                                }
                                else
                                {
                                    src.CopyTo(src.FullName.Replace(srcDir, destDir));
                                }
                                filesLeft--;
                                sizeLeft = sizeLeft - src.Length;
                                stopw.Stop();
                                State.GetInstance().Write(currentDateTime, template, true, src.FullName, src.FullName.Replace(srcDir, destDir), src.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                                Log.GetInstance().Write(template.backupName, src, new FileInfo(src.FullName.Replace(srcDir, destDir)), src.Length, stopw.Elapsed, cryptDuration);
                                stopw.Reset();
                            }
                        }
                    }
                    /* If file exists only in the src directory, it was created recently */
                    if (!wasCreated)
                    {

                        /* Creating files and folders if they doesn't exists */
                        new FileInfo(src.FullName.Replace(srcDir, destDir)).Directory.Create();
                        stopw.Start();
                        if (Path.GetExtension(src.ToString()) == ".txt")
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            if (File.Exists(@".\..\..\..\..\CryptoSoft\CryptoSoft\bin\Release\netcoreapp3.1\CryptoSoft.exe"))
                            {
                                startInfo.FileName = @".\..\..\..\..\CryptoSoft\CryptoSoft\bin\Release\netcoreapp3.1\CryptoSoft.exe";
                            }
                            else
                            {
                                startInfo.FileName = @"CryptoSoft.exe";
                            }
                            startInfo.ArgumentList.Add(src.ToString());
                            startInfo.ArgumentList.Add(src.FullName.Replace(srcDir, destDir));
                            startInfo.UseShellExecute = false;
                            startInfo.RedirectStandardOutput = true;
                            startInfo.CreateNoWindow = true;
                            Process currentProcess = Process.Start(startInfo);
                            currentProcess.WaitForExit();
                            cryptDuration = currentProcess.ExitCode.ToString();
                        }
                        else
                        {
                            src.CopyTo(src.FullName.Replace(srcDir, destDir));
                        }
                        filesLeft--;
                        sizeLeft = sizeLeft - src.Length;
                        stopw.Stop();
                        State.GetInstance().Write(currentDateTime, template, true, src.FullName, src.FullName.Replace(srcDir, destDir), src.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                        Log.GetInstance().Write(template.backupName, src, new FileInfo(src.FullName.Replace(srcDir, destDir)), src.Length, stopw.Elapsed, cryptDuration);
                        stopw.Reset();
                    }
                    wasCreated = false;
                }
                State.GetInstance().Write(currentDateTime, template, false, null, null, 0, totalSize, 0, totalFiles, 0, totalTime.Elapsed);
                totalTime.Stop();
            }
            else
            {
                throw new Exception("You need to execute at least one full save with the same source directory as your differential save");
            }
        }
    }
}
