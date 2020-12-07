using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using EasySave_V2.Properties;

namespace NSView
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            LoadAllLang();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            aboutUs1.Show();
        }

        /* Shows the about us User Control */
        private void AboutUs_Click(object sender, EventArgs e)
        {
            if (!aboutUs1.Visible)
            {
                HideAll();
                aboutUs1.Show();
            }
        }

        /* Shows the parameters User Control */
        private void parameters_Click(object sender, EventArgs e)
        {
            editConfig1.startMenu = this;
            if (!editConfig1.Visible)
            {
                HideAll();
                editConfig1.Show();
            }
            editConfig1.forbiddenProcesses = GraphicalView.controller.getForbiddenProcesses();
            editConfig1.extensionsToEncrypt = GraphicalView.controller.getExtensionsToEncrypt();
            editConfig1.ChangelistBox1();
            editConfig1.ChangelistBox2();
            editConfig1.BringToFront();
        }

        /* Open logs */
        private void OpenLogs_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.OpenLogs();
        }

        /* Shows the execute save template User Control */
        private void Execute_Click(object sender, EventArgs e)
        {
            if (!executeSaveTemplate1.Visible)
            {
                HideAll();
                executeSaveTemplate1.Show();
            }
            executeSaveTemplate1.templates = GraphicalView.controller.GetAllTemplates();
            executeSaveTemplate1.ChangelistBox();
            executeSaveTemplate1.BringToFront();
        }

        /* Shows the manage save template User Control */
        private void Manage_Click(object sender, EventArgs e)
        {
            if (!manageTemplate1.Visible)
            {
                HideAll();
                manageTemplate1.Show();
            }
            manageTemplate1.BringToFront();
        }

        private void HideAll()
        {
            executeSaveTemplate1.Hide();
            manageTemplate1.Hide();
            editConfig1.Hide();
            aboutUs1.Hide();
        }

        /* Print a popup message */
        public void PrintMessage(string message, int type)
        {
            if (type == -1)
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (type == 1)
                MessageBox.Show(message, "Operation success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /* Closes the application */
        private void Exit_Click(object sender, EventArgs e)
        {
            GraphicalView.controller.Exit();
        }
        /* Allows the use of multiple languages */
        public void LoadAllLang()
        {
            loadLang();
            executeSaveTemplate1.loadLang();
            manageTemplate1.loadLang();
            editConfig1.loadLang();
            aboutUs1.loadLang();

        }

        /* Allows the use of multiple languages */
        private void loadLang()
        {
            Manage.Text = Resources.Manage;
            button1.Text = Resources.Log;
            parameters.Text = Resources.Param;
            Exit.Text = Resources.Exit;
            Execute.Text = Resources.Exec;
            AboutUs.Text = Resources.About;
        }
    }
}
