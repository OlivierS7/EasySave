using System;
using System.Windows.Forms;

namespace NSView
{
    public partial class StartMenu : Form
    {
        private IView View;
        public StartMenu(IView View)
        {
            this.View = View;
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HideAll();
            manageTemplate1.Show();
            manageTemplate1.View = View;
            manageTemplate1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HideAll();
            executeSaveTemplate1.Show();
            executeSaveTemplate1.controller = View.Controller;
            executeSaveTemplate1.templates = View.Controller.GetAllTemplates();
            executeSaveTemplate1.ChangelistBox();
            executeSaveTemplate1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            View.Controller.OpenLogs();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            View.Controller.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HideAll();
            editConfig1.Show();
            editConfig1.controller = View.Controller;
            editConfig1.forbiddenProcesses = View.Controller.getForbiddenProcesses();
            editConfig1.extensionsToEncrypt = View.Controller.getExtensionsToEncrypt();
            editConfig1.ChangelistBox1();
            editConfig1.ChangelistBox2();
            editConfig1.BringToFront();
        }

        private void HideAll()
        {
            executeSaveTemplate1.Hide();
            manageTemplate1.Hide();
            editConfig1.Hide();
        }

        public void PrintMessage(string message, int type)
        {
            if (type == -1)
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (type == 1)
                MessageBox.Show(message, "Operation success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
