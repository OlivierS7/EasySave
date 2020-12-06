using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using EasySave_V2.Properties;

namespace NSView
{
    public partial class AboutUs : UserControl
    {

        /* Allows to open links with chrome */
        private const string ChromeAppKey = @"\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";

        public AboutUs()
        {
            InitializeComponent();
        }

        /* Link to GitHub */
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

        /* Open link with Chrome */
        public static void OpenLink(string url)
        {
            string chromeAppFileName = ChromeAppFileName;
            if (string.IsNullOrEmpty(chromeAppFileName))
            {
                throw new Exception("Could not find chrome.exe!");
            }
            Process.Start(chromeAppFileName, url);
        }

        private void AboutUs_Load(object sender, EventArgs e)
        {
            
        }

        /* Allows the use of multiple languages */
        public void loadLang()
        {
            label1.Text = Resources.Phrase9;
            label2.Text = Resources.Welcom;
            label3.Text = Resources.Phrase1;
            label4.Text = Resources.Phrase2;
            label5.Text = Resources.Phrase3;
            label6.Text = Resources.Phrase4;
            label7.Text = Resources.Phrase5;
            label8.Text = Resources.Phrase6;
            label9.Text = Resources.Phrase7;
            label10.Text = Resources.Phrase8;
        }
    }
}
