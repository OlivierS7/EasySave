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
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            delSaveTemplate1.Hide();
            creaSaveTemplate1.Show();
            creaSaveTemplate1.BringToFront();
            creaSaveTemplate1.controller = this.controller;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            creaSaveTemplate1.Hide();
            delSaveTemplate1.Show();
            delSaveTemplate1.BringToFront();
        }
    }
}
