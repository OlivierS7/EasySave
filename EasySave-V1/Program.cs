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
            backup.SetbackupName(Console.ReadLine());
            Console.WriteLine("Veuillez entrer le nom du dossier source :");
            backup.SetsrcDirectory(Console.ReadLine());
            Console.WriteLine("Veuillez entrer le nom du dossier de destination :");
            backup.SetdestDirectory(Console.ReadLine());
            Console.WriteLine("1 - Backup complète\n2 - Backup incrémentielle");
            backup.SetbackupType(int.Parse(Console.ReadLine()));
            backup.prepareCopy();
        }
      
    }
}
