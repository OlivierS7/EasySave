using System;
using System.Globalization;
using System.IO;
using System.Threading;
using NSController;

namespace EasySave_V1
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            Controller controller = new Controller();
        }
    }
}
