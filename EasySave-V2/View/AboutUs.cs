using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace EasySave_V2.View
{
    public partial class AboutUs : UserControl
    {

        private const string ChromeAppKey = @"\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";

        public AboutUs()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://github.com/OlivierS7/EasySave/tree/easysave/v2.0");
        }

        private static string ChromeAppFileName
        {
            get
            {
                return (string)(Registry.GetValue("HKEY_LOCAL_MACHINE" + ChromeAppKey, "", null) ??
                                    Registry.GetValue("HKEY_CURRENT_USER" + ChromeAppKey, "", null));
            }
        }

        public static void OpenLink(string url)
        {
            string chromeAppFileName = ChromeAppFileName;
            if (string.IsNullOrEmpty(chromeAppFileName))
            {
                throw new Exception("Could not find chrome.exe!");
            }
            Process.Start(chromeAppFileName, url);
        }
    }
}
