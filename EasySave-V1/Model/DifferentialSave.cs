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
                String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
                String TodaysTime = DateTime.Now.ToString("HH-mm-ss");
                string dateTime = Todaysdate + "_" + TodaysTime;
                string destDir = template.destDirectory + "\\" + dateTime;
                string srcDir = template.srcDirectory;
                string compDir = fullSave.destDirectory;
                string[] srcFiles = Directory.GetFiles(srcDir, ".", SearchOption.AllDirectories);
                string[] compFiles = Directory.GetFiles(compDir, ".", SearchOption.AllDirectories);
                bool wasCreated = false;
                foreach (string srcFile in srcFiles)
                {
                    FileInfo src = new FileInfo(srcFile);
                    foreach (string compFile in compFiles)
                    {
                        FileInfo comp = new FileInfo(compFile);
                        if (comp.Name == src.Name)
                        {
                            wasCreated = true;
                            if (comp.Length != src.Length)
                            {
                                new FileInfo(src.FullName.Replace(srcDir, destDir)).Directory.Create();
                                src.CopyTo(src.FullName.Replace(srcDir, destDir));
                                Console.WriteLine("Fichier édité: " + compFile);
                            }
                        }
                    }
                    if (!wasCreated)
                    {
                        new FileInfo(src.FullName.Replace(srcDir, destDir)).Directory.Create();
                        src.CopyTo(src.FullName.Replace(srcDir, destDir));
                    }
                    wasCreated = false;
                }
            }
        }
    }
}
