using System;
using System.Diagnostics;
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

                bool wasCreated = false;
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
                                /* Creating files and folders if they doesn't exists */
                                new FileInfo(src.FullName.Replace(srcDir, destDir)).Directory.Create();
                                stopw.Start();
                                src.CopyTo(src.FullName.Replace(srcDir, destDir));
                                filesLeft--;
                                sizeLeft = sizeLeft - src.Length;
                                stopw.Stop();
                                State.GetInstance().Write(currentDateTime, template, true, src.FullName, src.FullName.Replace(srcDir, destDir), src.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                                Log.GetInstance().Write(template.backupName, src, new FileInfo(src.FullName.Replace(srcDir, destDir)), src.Length, stopw.Elapsed);
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
                        src.CopyTo(src.FullName.Replace(srcDir, destDir));
                        filesLeft--;
                        sizeLeft = sizeLeft - src.Length;
                        stopw.Stop();
                        State.GetInstance().Write(currentDateTime, template, true, src.FullName, src.FullName.Replace(srcDir, destDir), src.Length, totalSize, sizeLeft, totalFiles, filesLeft, totalTime.Elapsed);
                        Log.GetInstance().Write(template.backupName, src, new FileInfo(src.FullName.Replace(srcDir, destDir)), src.Length, stopw.Elapsed);
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
