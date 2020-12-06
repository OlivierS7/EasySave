using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NSController;

namespace EasySave_V2
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
            Controller controller = new Controller();
            
        }
    }
}
