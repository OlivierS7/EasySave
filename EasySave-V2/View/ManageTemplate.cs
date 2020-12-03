using System;
using System.Windows.Forms;
using NSController;

namespace EasySave_V2.View
{
    public partial class ManageTemplate : UserControl
    {
        public Controller controller;
        public ManageTemplate()
        {
            InitializeComponent();
            creaSaveTemplate1.Hide();
            delSaveTemplate1.Hide();
            modifySaveTemplate1.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            delSaveTemplate1.Hide();
            modifySaveTemplate1.Hide();
            creaSaveTemplate1.controller = this.controller;
            creaSaveTemplate1.Show();
            creaSaveTemplate1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            creaSaveTemplate1.Hide();
            modifySaveTemplate1.Hide();
            delSaveTemplate1.controller = this.controller;
            delSaveTemplate1.templates = this.controller.GetAllTemplates();
            delSaveTemplate1.ChangelistBox1();
            delSaveTemplate1.Show();
            delSaveTemplate1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            creaSaveTemplate1.Hide();
            delSaveTemplate1.Hide();
            modifySaveTemplate1.controller = this.controller;
            modifySaveTemplate1.templates = this.controller.GetAllTemplates();
            modifySaveTemplate1.ChangelistBox1();
            modifySaveTemplate1.Show();
            modifySaveTemplate1.BringToFront();
        }       
    }
}
