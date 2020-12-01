using System;
using System.IO;

namespace CryptoSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(args[0]) && args[1] != null)
            {
                File.WriteAllBytes(args[1], XOR.GetInstance().EncryptOrDecrypt(args[0]));
            }
            else
            {
                throw new Exception("Cannot find the source file");
            }
        }
    }
}
