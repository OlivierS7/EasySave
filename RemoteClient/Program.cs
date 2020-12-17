using System;
using RemoteClient.NSController;

namespace RemoteClient
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Controller controller = new Controller();
        }
    }
}
