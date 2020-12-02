using System;
using System.IO;

namespace CryptoSoft
{
    class Program
    {
        static int Main(string[] args)
        {
            if (File.Exists(args[0]) && args[1] != null)
            {
                return XOR.GetInstance().EncryptOrDecrypt(args[0], args[1]);
            }
            else
            {
                return -1;
            }
        }
    }
}
