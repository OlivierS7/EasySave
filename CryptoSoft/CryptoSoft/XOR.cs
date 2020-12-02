using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptoSoft
{
    public class XOR
    {
        private byte[] key;
		private static XOR xor;

		private XOR()
		{
			string path = @".\Key.txt";
			if (File.Exists(path))
			{
				key = File.ReadAllBytes(path);
			}
			else
			{
				throw new Exception("Cannot find Key.txt");
			}
		}

		/* Method to get Instance of this Singleton*/
		public static XOR GetInstance()
		{
			if (xor == null)
			{
				xor = new XOR();
			}
			return xor;
		}
		public byte[] EncryptOrDecrypt(string source)
        {
			byte[] text = File.ReadAllBytes(source);
            byte[] xor = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                xor[i] = (byte)(text[i] ^ key[i % key.Length]);
            }
            return xor;
        }
    }
}
