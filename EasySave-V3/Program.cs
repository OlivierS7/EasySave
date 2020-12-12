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
            bool createdNew;
            new Mutex(true, "EasySaveRunning", out createdNew);
            if (createdNew)
            {
                Controller controller = new Controller();
            }
            else
                MessageBox.Show("An EasySave Process is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
