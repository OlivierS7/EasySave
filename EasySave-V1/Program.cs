using System;
using Save;

namespace EasySave_V1
{
    class Program
    {
        static void Main(string[] args)
        {

            EasySave easySave = new EasySave();
            easySave.Initialize();
            
        }
    }

    class EasySave
    {
        public void Initialize()
        {
            Backup backup = new Backup();
            Console.WriteLine("Veuillez entrer le nom de la sauvegarde :");
            backup.backupName = (Console.ReadLine());
            Console.WriteLine("Veuillez entrer le nom du dossier source :");
            backup.srcDirectory = (Console.ReadLine());
            Console.WriteLine("Veuillez entrer le nom du dossier de destination :");
            backup.destDirectory = (Console.ReadLine());
            Console.WriteLine("1 - Backup complète\n2 - Backup incrémentielle");
            backup.backupType = (int.Parse(Console.ReadLine()));
            backup.prepareCopy();
        }
      
    }
}
